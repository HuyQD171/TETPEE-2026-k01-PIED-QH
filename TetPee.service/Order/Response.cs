namespace TetPee.service.Order;

public class Response
{
    public class CreateOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount{ get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string Description { get; set; }
        public string QRCode { get; set; }
    }
}