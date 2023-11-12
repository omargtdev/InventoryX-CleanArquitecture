using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InventoryX_CleanArquitecture.API.Controllers;

public class ErrorsController : ControllerBase
{
    // Ignore in swagger
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]

    public IActionResult HandleError()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}
