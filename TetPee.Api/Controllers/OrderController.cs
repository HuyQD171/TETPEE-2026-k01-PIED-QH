using Microsoft.AspNetCore.Mvc;
using TetPee.service.Models;
using TetPee.service.Order;

namespace TetPee.Api.Controllers;


[ApiController]
[Route("[controller]")]

public class OrderController : ControllerBase
{
    private readonly IService _service;

    public OrderController(IService service)
    {
        _service = service;
    }

    [HttpPost("Order")]
    public async Task<IActionResult> CreateCart(Request.CreateOrderRequest request)
    {
        var rs = await _service.CreateOrder(request);
        return Ok(ApiResponseFactory.SuccessResonse(rs, "order created", HttpContext.TraceIdentifier));
    }

    [HttpPost(template: "sepay/webhook")]
    public async Task<IActionResult> SepayWebhook(Request.SepayWebhookRequest request)
    {
      await _service.SepayWebhookHandler(request);
      return Ok(ApiResponseFactory.SuccessResonse("", "Webhook response", HttpContext.TraceIdentifier));
    }
        
}