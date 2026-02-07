using EconoHome.Application.Features.Transactions.Commands;
using EconoHome.Application.Features.Transactions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EconoHome.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/transactions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _mediator.Send(new GetAllTransactionsQuery());
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        // Se a pessoa for menor de idade e tentar cadastrar receita, 
        // o Handler ou a Entidade vai disparar uma exceção que você pode tratar aqui.
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTransactionCommand(id));
        return NoContent();
    }
}