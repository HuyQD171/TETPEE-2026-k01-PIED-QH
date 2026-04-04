using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TetPee.service.Cart;
using TetPee.service.Models;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController: ControllerBase
{
    private readonly IService _service;

    public CartController(IService service)
    {
        _service = service;
    }
    
    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateCart()
    {
        await _service.CreateCart();
        return Ok(ApiResponseFactory.SuccessResonse(null, "cart created", HttpContext.TraceIdentifier));
    }

    [HttpPost("product")]
    public async Task<IActionResult> AddProductToCart(Request.AddProductToCartRequest request)
    {
        await _service.AddProductToCart(request);
        return Ok(ApiResponseFactory.SuccessResonse("Successful!", "Product Add to Cart", HttpContext.TraceIdentifier));
    }
    
    [HttpDelete("")]
    public async Task<IActionResult> DeleteCart(Request.RemoveProductFromCartRequest request)
    {
        await _service.RemoveProductFromCart(request);
        return Ok(ApiResponseFactory.SuccessResonse("successful", "Product Add to Cart", HttpContext.TraceIdentifier));
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetProductFromCart()
    {
        var rs = await _service.GetCart();
        return Ok(ApiResponseFactory.SuccessResonse(rs, "Product Add to Cart", HttpContext.TraceIdentifier));
    }
}