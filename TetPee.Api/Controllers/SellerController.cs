using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.service.Seller;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class SellerController : ControllerBase
{
    
    private readonly AppDbContext _dbContext;
    private readonly IService _sellerService;

    public SellerController(AppDbContext dbContext,  IService sellerService)
    {
        _dbContext = dbContext;
        _sellerService = sellerService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllSeller(int pageSize = 10, int pageIndex = 1, string? searchTerm = null)
    {
        var results = await _sellerService.GetAllSeller(searchTerm, pageSize, pageIndex);
        return Ok(results);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllSellerById(Guid id)
    {
        var results = await _sellerService.GetSellerById(id);
        return Ok(results);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateSeller(Request.CreateSellerRequest request)
    {
        try
        {
            var result = await _sellerService.CreateSeller(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}