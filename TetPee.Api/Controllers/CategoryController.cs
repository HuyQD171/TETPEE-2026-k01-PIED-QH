using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.service.User;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    //cái này nâng cao lúc sau sẽ giải thích
    public CategoryController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
   
    [HttpGet(template:"")]
    public IActionResult GetCategory([FromQuery] string? searchTerm)
    {
        var users = _dbContext.Users.ToList();
        return Ok(users);
    }
    [HttpPost(template:"")]
    public IActionResult CreateCategory([FromBody] Request.CreateCategoryRequest request)
    {
        // var users = _dbContext.Users.ToList();
        var categories = new Category()
        {
            Name =  request.Name
        };
        
        _dbContext.Categories.Add(categories);
        
        _dbContext.SaveChanges();
        
        Console.WriteLine(request);
        return Ok("Create category success");
    }
    
    [HttpPut(template:"{id}")]
    public IActionResult UpdateCategory(Guid id,  [FromBody] Request.UpdateCategoryRequest request)
    {
        // var users = _dbContext.Users.ToList();
        var cate = _dbContext.Categories.Find(id);
        if (cate == null)
        {
            Ok("No user found");
        }
        else
        {
            cate.Name = request.Name;
        }
        return Ok("Update successful");
    }
    
    [HttpDelete(template:"{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        // var users = _dbContext.Users.ToList();
        var cate = _dbContext.Categories.Find(id);
        if (cate == null)
        {
            Ok("No user found");
        }
        else
        {
            cate.IsDeleted = true;
        }         
        return Ok("Delete user");
    }
}