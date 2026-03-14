using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.service.Categogy;


namespace TetPee.Api.Controllers;


public class CategoryController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _categoryService;


    //cái này nâng cao lúc sau sẽ giải thích
    public CategoryController(AppDbContext dbContext,  IService cateService)
    {
        _dbContext = dbContext;
        _categoryService = cateService;
    }
    
   
    [HttpGet("")]
    public async Task<IActionResult> GetAllCategory([FromQuery] string? searchTerm)
    {
        var results = await _categoryService.GetCategories();
        return Ok(results);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllChildrenCategory([FromRoute] Guid id)
    {
        var results = await _categoryService.GetAllChildrenCategory(id);
        return Ok(results);
    }
    /*[HttpPost("")]
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
    }*/
    
    /*[HttpPut("{id}")]
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
    
    [HttpDelete("{id}")]
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
    }*/
}