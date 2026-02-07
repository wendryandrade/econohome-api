using EconoHome.Application.Features.Persons.Commands;
using EconoHome.Application.Features.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EconoHome.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/persons
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }

        // GET: api/persons
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(persons);
        }

        // GET: api/persons/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            return Ok(person);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _mediator.Send(new GetPersonsSummaryQuery());
            return Ok(summary);
        }

        // PUT: api/persons/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonCommand command)
        {
            if (id != command.Id)
                return BadRequest("O ID da URL não corresponde ao ID do comando.");

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/persons/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
            return NoContent();
        }
    }
}