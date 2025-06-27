using MediatR;
using MediatRApi.Application.Commands;
using MediatRApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatRApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController(IMediator mediator) : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var response = await mediator.Send(new GetProductsQuery());
      return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreateProductCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
  }
}
