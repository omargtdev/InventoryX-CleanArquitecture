using InventoryX_CleanArquitecture.Application.Clients.Create;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        var createCustomerResult = await _mediator.Send(command);

        return createCustomerResult.Match(
            customer => Ok(createCustomerResult.Value),
            errors => Problem(errors));
    }
}
