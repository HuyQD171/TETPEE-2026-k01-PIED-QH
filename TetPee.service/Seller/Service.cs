using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TetPee.Repository;

namespace TetPee.service.Seller;

public class Service : IService
{
    
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext) //DI
    {
        _dbContext = dbContext;
    }
    
    public async Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(
        string? searchTerm,
        int pageSize = 10,
        int pageIndex = 1)
    {
            var query = _dbContext.Sellers.Where(x => true);

            query = query.OrderBy(x => x.User.FirstName);
        
            query = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            var selectedQuery = query
                .Select(x => new Response.GetSellerResponse()
                {
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImageUrl = x.User.ImageUrl,
                    TaxCode =  x.TaxCode,
                    CompanyName = x.CompanyName
                });


            var listResult = await selectedQuery.ToListAsync();
            var totalItems = listResult.Count();

            var result = new Base.Response.PageResult<Response.GetSellerResponse>()
            {
                Items = listResult,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
            };
            return result;
        }

    public Task<Base.Response.PageResult<Response.GetSellerResponse>> GetSellerById(Guid id)
    {
        throw new NotImplementedException();
    }
}