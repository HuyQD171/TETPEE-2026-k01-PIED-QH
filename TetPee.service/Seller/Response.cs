namespace TetPee.service.Seller;

public class Response
{
    public class GetSellerResponse : User.Response.GetAllUserResponse
    {
        public string? TaxCode { get; set; }
        public string? CompanyName { get; set; }
    }
    
    public class GetSellerByIdResponse : User.Response.GetUsersResponse
    {
        public string? TaxCode { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
    }
}