using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TetPee.Repository;
using TetPee.Repository.Entity;

namespace TetPee.service.Order;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public async Task<Response.CreateOrderResponse> CreateOrder(Request.CreateOrderRequest request)
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        
        var userIdGuid = Guid.Parse(userId!);
        
        //list Object -> List Guid
        var productIds = request.Products.Select(x => x.ProductId).Distinct().ToList();

        var query = _dbContext.Products.Where(x => productIds.Contains(x.Id));
        
        var productCount = await query.CountAsync();

        if (productCount != productIds.Count)
        {
            throw new Exception("Some products not found");
        }

        var result = await query.ToListAsync();
        
        decimal totalAmount = 0;

        foreach (var product in result)
        {
            //tìm trong list  giỏ hàng sản phảm để tìm sô lượng mà khách hàng muốn mua
            var quantity = request.Products.First(x => x.ProductId == product.Id).Quantity;
            
            if (quantity <= 0)
            {
                throw new Exception($"Quantity of product {product.Id} must be greater than 0");
            }
            
            totalAmount += quantity * product.Price;
        }

        if (totalAmount <= 0)
        {
            throw new Exception("total amount must be greater than 0");
        }
 
        var order = new Repository.Entity.Order()
        {
            Id = Guid.NewGuid(),
            UserId = userIdGuid,
            TotalAmount = totalAmount,
            Address = request.Address,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
        };
        
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        List<OrderDetail> orderDetails = new List<OrderDetail>();
        foreach (var product in result)
        {
            var quantity = request.Products.First(x => x.ProductId == product.Id).Quantity;

            var orderDetail = new OrderDetail()
            {
                Id = Guid.NewGuid(),
                OrderID = order.Id,
                ProductID = product.Id,
                Quantity = quantity,
                UnitPrice = product.Price,
            };
            orderDetails.Add(orderDetail);
        }
        
        if (orderDetails.Any())
        {
            _dbContext.AddRange(orderDetails);
            await _dbContext.SaveChangesAsync();
        }
        


        foreach (var productId in productIds)
        {
            var queryOrderDetails = _dbContext.OrderDetails.Where(
                x => x.ProductID == productId &&  x.OrderID == order.Id);
        }
        
        
        
        string description = $"TETPEE-{order.Id}";

        var response = new Response.CreateOrderResponse()
        {
            OrderId = order.Id,
            TotalAmount = order.TotalAmount,
            BankName = "MBBank",
            BankAccount = "VQRQAIDAX4356",
            Description = description,
            QRCode = ""
        };

        string qrCode = $"https://qr.sepay.vn/img?" +
                        $"acc={response.BankAccount}&" +
                        $"bank={response.BankName}&" +
                        $"amount={(int)totalAmount}&" +
                        $"des={description}&" +
                        $"template=qronly";
        
        response.QRCode = qrCode;
        return response;

    }
    
    /*"gateway": "BIDV",
"transactionDate": "2026-04-06 23:41:15",
"accountNumber": "8886369921",
"subAccount": "96247BENTRAN",
"code": "TCMPBf9c3895c14b94583bad78673263", //TETPEEORDERID
"content": "QR - TCMPBf9c3895c14b94583bad786732631b1ca",
"transferType": "in",
"description": "BankAPINotify QR - TCMPBf9c3895c14b94583bad786732631b1ca",
"transferAmount": 2500,
"referenceCode": "bc8af415-13e4-4bf9-8352-a8af59df5808",
"accumulated": 0,
"id": 48628369*/

    public async Task SepayWebhookHandler(Request.SepayWebhookRequest request)
    {
        var description = request.Code; // ORDERID

        var raw = description.Replace("TETPEE", "");

        Guid? orderId = null;

        if (raw.Length == 32)
            // Mặc định 1 Guid có 32 kía tự nesuieu bklhongo có dauyas gác nổi,
        //
        {
            var formatted = $"{raw.Substring(startIndex: 0, length: 8)}-" +
                                   $"{raw.Substring(startIndex: 8, length: 4)}-" +
                                   $"{raw.Substring(startIndex: 12, length: 4)}-" +
                                   $"{raw.Substring(startIndex: 16, length: 4)}-" +
                                   $"{raw.Substring(startIndex: 20, length: 12)}";

            if (Guid.TryParse(formatted, out var guid))
            {
                orderId = guid;
            }
        }
        else
        {
            throw new Exception("Invalid description format");
        }

        var query = _dbContext.Orders
            .Where(x => x.Id == orderId)
            .Include(x => x.OrderDetails);

        var order = await query.FirstOrDefaultAsync();

        if(order == null)
        {
            throw new Exception("Order not found");
        }

        if(order.Status != "Pending")
        {
            throw new Exception("Order already processed");
        }

        if(order.TotalAmount != request.TransferAmount)
        {
            throw new Exception("Invalid transfer amount");
        }

        order.Status = "Completed";
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();
        
        var productIds = order.OrderDetails.Select(x => x.ProductID).ToList();

        
        //tìm những sản ohaamr chứa trong cart vowisw cái Id sau ProductIds ủa UserId
        var queryProdCart = _dbContext.CartDetails.Where(x =>
            x.Cart.UserId == order.UserId &&
            productIds.Contains(x.ProductID)
        );
        
        var removeCartDetails = await queryProdCart.ToListAsync();
        
        _dbContext.CartDetails.RemoveRange(removeCartDetails);
        
        await _dbContext.SaveChangesAsync();
        
        //tìm dc rồi thì xóa đi
        // _dbContext.RemoveRage
    }
}

//bản chát của SePay
//sẽ là 1 thằng ngồi lắng nghe hết tất cả các giao dịch của mình trong tài khoản
//và mình cvos thể làm 1 thứ, nếu có giao dihcj nào chuyển đến thì gọi 1 Api CallBack
//và không phải giao dihcj nào cũng là giao dihgjc của hệ thông mình 
    //Giao dịch trả tiền nợ từ bạn A -> Call Api
    //Giao dihcj mua hangf từ hệ thống TetPee -> Call Api
    //Giao dịch trá tiền cổ tức -> Call Api
    
    // Call Api của ai thì tùy mọi người setup voiwis hệ thống của mình, nhưng ở đya
     //a muốn là nó call api của mình dẻ tb chuyển khoản thaành công
     
    //Không phải tát cả acacs giapo dịch nào cũng là giao diuhgcj của hệ thống mình, vậy thì để
        //phan biệt giao dịch của mình, thjif chúng ta sẽ tạo ra 1 dấu ấn riêng, làm dấu