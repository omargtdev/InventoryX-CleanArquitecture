using InventoryX_CleanArquitecture.Application.Clients.Create;
using InventoryX_CleanArquitecture.Application.Clients.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryX_CleanArquitecture.API.Controllers;

[Route("[controller]")]
public class ClientsController : ApiController
{
    private readonly ISender _mediator;

    public ClientsController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllClientsResult = await _mediator.Send(new GetAllClientsQuery());

        return getAllClientsResult.Match(
            client => Ok(getAllClientsResult),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var createClientResult = await _mediator.Send(command);

        return createClientResult.Match(
            client => Ok(createClientResult.Value),
            errors => Problem(errors));
    }
}
