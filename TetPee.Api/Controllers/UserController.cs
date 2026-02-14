using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.service.User;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    //cái này nâng cao lúc sau sẽ giải thích
    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // HTTP Method: GET, POST, DELETE, PUT, PATCH
    // PARAM: Query string, Path Param, Body Param
    
    
    //Query String: http://localhost:5000/User?name=abc&age=20
    // name va age là query string
    // Query string nằm sau dấu hỏi ?
    //Path(Route) Param: http://localhost:5000/User/123
    //123 là path pram hoact route praram
    //Path param nằm trong đường dẫn
    
  //GET là không có body
    //POST ,PUT, PATCH có body
    
    //Tại sao phải dùng body: tránh để lộ những thông tin không mong muốn 
    //Ví dụ: Username,Pass
    //ko thể http://localhost:5000/login?username=abc&password=123
    
    //Chuẩn REST FUll API
    //get all users http://localhost:5000/User
    //get user by id: GET http://localhost:5000/User/{id}
    //update user by id: PUT http://localhost:5000/User/{id}
    //delete user by id : DELETE http://localhost:5000/User/{id}
    [HttpGet(template:"")]
    public IActionResult GetUsers([FromQuery] string? searchTerm)
    {
        var users = _dbContext.Users.ToList();
        throw new Exception("Get all users Errors");
        return Ok(users);
    }
    
    [HttpGet(template:"{id}")]
    public IActionResult GetUsersByid(Guid id)
    {
        // var users = _dbContext.Users.ToList();
        return Ok("Get all users");
    }
    
    [HttpPost(template:"")]
    public IActionResult CreateUsers([FromBody] Request.CreateUserRequest request)
    {
        // var users = _dbContext.Users.ToList();
        var user = new User()
        {
            Email = request.Email,
            FirstName = request.Firstname,
            LastName = request.Lastname,
            HashedPassword = request.Password //chuaw hash, chir demo
        };
        
        _dbContext.Users.Add(user);
        
        _dbContext.SaveChanges();
        
        Console.WriteLine(request);
        return Ok("Get all users");
    }
    
    [HttpPut(template:"{id}")]
    public IActionResult UpdateUser(Guid id,  [FromBody] Request.UpdateUserRequest request)
    {
        // var users = _dbContext.Users.ToList();
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            Ok("No user found");
        }
        else
        {
            user.Email = request.Email;
            user.FirstName = request.Firstname;
            user.LastName = request.Lastname;
            user.HashedPassword = request.Password;
        }
        return Ok("Update successful");
    }
    
    [HttpDelete(template:"{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        // var users = _dbContext.Users.ToList();
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            Ok("No user found");
        }
        else
        {
            user.IsDeleted = true;
        }            
        return Ok("Delete user");
    }
}