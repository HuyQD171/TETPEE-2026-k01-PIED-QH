namespace TetPee.service.Seller;

public class Response
{
    public class GetSellerResponse
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        
        public string TaxCode { get; set; }
        public string CompanyName { get; set; }
    }
}