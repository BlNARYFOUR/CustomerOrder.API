using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(ISender requestBus) : ControllerBase
{
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(int id)
    {
        await _requestBus.Send(new OrderCancelCommand(id));

        return NoContent();
    }
    // todo
}
