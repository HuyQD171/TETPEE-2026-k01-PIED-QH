using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TetPee.Api.Extensions;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.service.Product;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _productService;

    public ProductController(AppDbContext dbContext, IService productService)
    {
        _dbContext = dbContext;
        _productService = productService;
    }

    /*
    [Authorize(Policy = JwtExtensions.SellerPolicy)]
    */
    [HttpPost("")]
    public async Task<IActionResult> CreateSeller(Request.CreateProductRequest request)
    {
            var result = await _productService.CreateProduct(request);
            return Ok(result);
    }
}