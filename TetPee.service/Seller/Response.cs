namespace TetPee.service.Seller;

public class Response
{
    public class GetSellerResponse : User.Response.GetUsersResponse
    {
        
        
        public string? TaxCode { get; set; }
        public string? CompanyName { get; set; }
    }
    
    public class GetAllSellerResponse : User.Response.GetAllUserResponse
    {
        
    }
}