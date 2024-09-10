﻿using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/customers/{customerId}/orders")]
[ApiController]
public class CustomerOrdersController(
    ISender requestBus,
    IOrderMapper mapper,
    IOrderListMapper listMapper
) : ControllerBase {
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
    private readonly IOrderMapper _orderMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IOrderListMapper _orderListMapper = listMapper ?? throw new ArgumentNullException(nameof(listMapper));

    [HttpGet]
    public async Task<ActionResult<OrderGet>> GetList(int customerId)
    {
        return Ok(_orderListMapper.ToDto(await _requestBus.Send(
            new OrderGetListForCustomerQuery(customerId)
        )));
    }

    // todo
}
