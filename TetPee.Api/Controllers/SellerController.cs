using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TetPee.Repository.Entity;
using TetPee.service.Seller;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class SellerController : ControllerBase
{
    
    private readonly DbContext _dbContext;
    private readonly IService _sellerService;

    public SellerController(DbContext dbContext,  IService sellerService)
    {
        _dbContext = dbContext;
        _sellerService = sellerService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllSeller([FromQuery] string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var results = await _sellerService.GetAllSeller(searchTerm, pageSize, pageIndex);
        return Ok(results);
    }
    /*public async Task<IActionResult> GetAllSellerById([FromQuery] string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var results = await _sellerService.GetAllSeller(searchTerm, pageSize, pageIndex);
        return Ok(results);
    }*/
}