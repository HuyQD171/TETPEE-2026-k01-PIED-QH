using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.service.Categogy;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    
    public Service(AppDbContext dbContext) //DI
    {
        _dbContext = dbContext;
    }
    public async Task<List<Response.GetCategoriesResponse>> GetCategories()
    {
        var query = _dbContext.Categories.Where(x => true);

        query = query.OrderBy(x => x.Name);

        var selectQuery = query.Select(x => new Response.GetCategoriesResponse()
        {
            Id = x.Id,
            Name = x.Name,
        });
        
        var result = await selectQuery.ToListAsync();
        
        return result;
    }

    public async Task<List<Response.GetCategoriesResponse>> GetAllChildrenCategory(Guid id)
    {
        var query = _dbContext.Categories.Where(x => x.ParentId == id);

        query = query.OrderBy(x => x.Name);

        var selectQuery = query.Select(x => new Response.GetCategoriesResponse()
        {
            Id = x.Id,
            Name = x.Name,
        });
        
        var result = await selectQuery.ToListAsync();

        return result;
    }
}