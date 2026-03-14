using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.service.User;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext) //DI
    {
        _dbContext = dbContext;
    }

    public async Task<service.Base.Response.PageResult<Response.GetUsersResponse>> GetUsers(
        string? searchTerm,
        int pageSize,
        int pageIndex)
    {
        var query = _dbContext.Users.Where(x => true);
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm)
                || x.LastName.Contains(searchTerm)
                || x.Email.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.Email);

        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query
            .Select(x => new Response.GetUsersResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ImageUrl = x.ImageUrl,
                Role = x.Role,
                Address = x.Address,

            });


        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new service.Base.Response.PageResult<Response.GetUsersResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        return result;
    }

    public async Task<Response.GetUsersResponse?> GetUserById(Guid id)
    {
        var queryId = _dbContext.Users.Where(x => x.Id == id);

        var selectedQuery = queryId
            .Select(x => new Response.GetUsersResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ImageUrl = x.ImageUrl,
                Role = x.Role,
                Address = x.Address,

            });
        var result = await selectedQuery.FirstOrDefaultAsync();
        return result;
    }

}