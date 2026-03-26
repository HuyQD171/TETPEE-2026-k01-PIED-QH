using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.service.Seller;

public class Service : IService
{
    
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext) //DI
    {
        _dbContext = dbContext;
    }
    public async Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(string? searchTerm, int pageSize, int pageIndex)
    {
        var query = _dbContext.Sellers.Where(x => true);
        
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.User.FirstName.Contains(searchTerm)
                || x.User.LastName.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.User.FirstName);

        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query
            .Select(x => new Response.GetSellerResponse()
            {
                Id = x.Id,
                Email = x.User.Email,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                ImageUrl = x.User.ImageUrl,
                Role = x.User.Role,
                CompanyName = x.CompanyName,
                TaxCode =  x.TaxCode

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
        /*var query = _dbContext.Users.Where(x => x.Role == "Seller");
        
        /*
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm)
                || x.LastName.Contains(searchTerm)
                || x.Email.Contains(searchTerm));
        }
        #1#

        query = query.OrderBy(x => x.Email);

        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query
            .Select(x => new Response.GetSellerResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImageUrl,
                Role = x.Role,
                CompanyName = x.Seller!.CompanyName,
                TaxCode =  x.Seller.TaxCode

            });


        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new service.Base.Response.PageResult<Response.GetSellerResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        Console.WriteLine(result);
        return result;*/
    }

    public async Task<Response.GetSellerByIdResponse> GetSellerById(Guid id)
    {
        var query = _dbContext.Sellers.Where(x => x.Id == id);

        var selectedQuery = query
            .Select(x => new Response.GetSellerByIdResponse()
            {
                Id =  x.Id,
                Email = x.User.Email,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                ImageUrl = x.User.ImageUrl,
                PhoneNumber = x.User.PhoneNumber,
                Address = x.User.Address,
                CompanyName = x.CompanyName,
                TaxCode =  x.TaxCode,
                CompanyAddress =  x.CompanyAddress,
                

            });
        var result = await selectedQuery.FirstOrDefaultAsync();

        return result;    
    }


    public async Task<string> CreateSeller(Request.CreateSellerRequest request)
    {
        var existingUserQuery = _dbContext.Users.Where(x => x.Email == request.Email);
        
        bool isExistUser = await existingUserQuery.AnyAsync();
        
        if(isExistUser)
        {
            throw new Exception(Message.UserExistWithMail);
        }
        
        var user = new Repository.Entity.User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            HashedPassword = request.HashedPassword,
            Role = "Seller"
        };

        _dbContext.Add(user);

        var result = await _dbContext.SaveChangesAsync();
        
        if (result > 0)
        {
            var seller = new Repository.Entity.Seller()
            {
                CompanyAddress = request.CompanyAddress,
                CompanyName = request.CompanyName,
                TaxCode = request.TaxCode,
                UserId = user.Id,
            };
            
            _dbContext.Add(seller);
            
            var sellerResult = await _dbContext.SaveChangesAsync();

            if (sellerResult > 0) return "Add Seller successfully";
            
            return Message.FailToAddSeller;
        }
        
        return Message.FailToAddSeller;
    }

}