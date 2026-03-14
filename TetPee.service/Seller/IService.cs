namespace TetPee.service.Seller;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(
        string? searchTerm,
        int pageSize = 10,
        int pageIndex = 1);
    
    public Task<Base.Response.PageResult<Response.GetSellerResponse>> GetSellerById(Guid id);
}