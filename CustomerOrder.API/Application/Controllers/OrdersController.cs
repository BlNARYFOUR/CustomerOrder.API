using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Requests.Commands;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(ISender requestBus, IOrderListMapper listMapper) : ControllerBase
{
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
    private readonly IOrderListMapper _orderListMapper = listMapper ?? throw new ArgumentNullException(nameof(listMapper));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderGet>>> GetList([FromQuery] List<int> customerIds, string? from, string? to)
    {
        return Ok(_orderListMapper.ToDto(await _requestBus.Send(
            new OrderSearchListQuery(customerIds, from, to)
        )));
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(int id)
    {
        await _requestBus.Send(new OrderCancelCommand(id));

        return NoContent();
    }
}
