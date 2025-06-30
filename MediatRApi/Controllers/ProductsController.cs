using MediatR;
using MediatRApi.Application.Commands;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatRApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController(IMediator mediator) : ControllerBase
  {
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductResponse>>(200)]
    public async Task<IActionResult> Get()
    {
      var response = await mediator.Send(new GetProductsQuery());
      return Ok(response);
    }
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Post([FromForm] CreateProductCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
  }
}
