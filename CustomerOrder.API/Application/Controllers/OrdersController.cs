using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(
    ISender requestBus,
    IOrderMapper mapper,
    IOrderListMapper listMapper
) : ControllerBase {
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
    private readonly IOrderMapper _orderMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IOrderListMapper _orderListMapper = listMapper ?? throw new ArgumentNullException(nameof(listMapper));

    [HttpGet]
    public async Task<ActionResult<OrderGet>> GetList()
    {
        return Ok(_orderListMapper.ToDto(await _requestBus.Send(new OrderGetListQuery())));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<OrderGet>>> GetById(int id)
    {
        return Ok(_orderMapper.ToDto(await _requestBus.Send(new OrderGetByIdQuery(id))));
    }

    // todo
}
