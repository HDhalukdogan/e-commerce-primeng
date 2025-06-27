using MediatR;
using MediatRApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace MediatRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoEntitiesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetDemoEntitiesQuery());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetDemoEntityByIdQuery { Id = id });
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDemoEntityCommand command)
        {
            var entity = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDemoEntityCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var entity = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteDemoEntityCommand { Id = id });

            return response ? NoContent() : BadRequest("Not deleted");
        }
    }
}
