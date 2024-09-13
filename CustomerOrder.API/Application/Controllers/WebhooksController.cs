using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/webhooks")]
[ApiController]
[ProducesResponseType(StatusCodes.Status204NoContent)]
public class WebhooksController(ISender requestBus) : ControllerBase
{
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

    [HttpPost("email/failed")]
    public async Task<ActionResult> EmailFailedWebhhook(EmailWebhookUpsert dto)
    {
        await _requestBus.Send(new EmailResendCommand(dto.Token));

        return NoContent();
    }
}
