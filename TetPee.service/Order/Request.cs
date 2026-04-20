namespace TetPee.service.Order;

public class Request
{
    public class CreateOrderRequest
    {
        public string Address { get; set; }
        public string PhoneNumbder { get; set; }
        public List<ProductOrderRequest> Products { get; set; }
        
    }
    
    public class ProductOrderRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
    
    
    //tạo đơn hàng(giả sử hệ thông mình muốn)
    //setup chuyeerrn khoản thành công (bằng tiền thiệt), đẻ đơn hàng đã dc đăt
        //nếu tạo ra đơn hàng mà ko chuyern klhoanr leiefn thì đơn hàng sẽ bị hũy sao 15p,
            //để tránh tình trạng kahcsh hàng đặt ràng rồi không chuyển khoản, almf ảnh huiongw đến việc quản lý dơn hàng
    
    //Huy đơn hàng
    //cap nhat don hàng
    
    public class SepayWebhookRequest
    {
        public string Gateway { get; set; }
        public string TransactionDate { get; set; }
        public string AccountNumber { get; set; }
        public string SubAccount { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string TransferType { get; set; }
        public string Description { get; set; }
        public decimal TransferAmount { get; set; }
        public string ReferenceCode { get; set; }
        public decimal Accumulated { get; set; }
        public long Id { get; set; }
    }
    
    /*"gateway": "BIDV",
    "transactionDate": "2026-04-06 23:41:15",
    "accountNumber": "8886369921",
    "subAccount": "96247BENTRAN",
    "code": "TCMPBf9c3895c14b94583bad78673263",
    "content": "QR - TCMPBf9c3895c14b94583bad786732631b1ca",
    "transferType": "in",
    "description": "BankAPINotify QR - TCMPBf9c3895c14b94583bad786732631b1ca",
    "transferAmount": 2500,
    "referenceCode": "bc8af415-13e4-4bf9-8352-a8af59df5808",
    "accumulated": 0,
    "id": 48628369*/
}