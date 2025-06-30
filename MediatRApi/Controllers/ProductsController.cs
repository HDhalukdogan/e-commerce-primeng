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
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put(UpdateProductCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete(int id)
    {
      await mediator.Send(new DeleteProductCommand(id));
      return NoContent();
    }
    [HttpPost("{id:int}/images")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostImage(int id, IFormFile file)
    {
      using var memoryStream = new MemoryStream();

      await file.CopyToAsync(memoryStream);

      await mediator.Send(new CreateProductImageCommand()
      {
        ProductId = id,
        FileName = file.FileName,
        Image = memoryStream.ToArray()
      });

      return Created();
    }
  }
}
