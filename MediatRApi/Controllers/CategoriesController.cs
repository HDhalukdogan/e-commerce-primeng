using MediatR;
using MediatRApi.Application.Commands;
using MediatRApi.Application.Common.Responses;
using MediatRApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatRApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController(IMediator mediator) : ControllerBase
  {
    [HttpGet]
    [ProducesResponseType<IEnumerable<CategoryResponse>>(200)]
    public async Task<IActionResult> Get()
    {
      var response = await mediator.Send(new GetCategoriesQuery());
      return Ok(response);
    }
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Post([FromForm] CreateCategoryCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put([FromForm] UpdateCategoryCommand command)
    {
      await mediator.Send(command);
      return NoContent();
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete(int id)
    {
      await mediator.Send(new DeleteCategoryCommand(id));
      return NoContent();
    }
  }
}
