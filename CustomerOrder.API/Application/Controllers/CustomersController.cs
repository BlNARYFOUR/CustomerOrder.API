﻿using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController(
    ISender requestBus,
    ICustomerMapper mapper,
    ICustomerListMapper listMapper
) : ControllerBase {
    private readonly ISender _requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
    private readonly ICustomerMapper _customerMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICustomerListMapper _customerListMapper = listMapper ?? throw new ArgumentNullException(nameof(listMapper));

    [HttpGet]
    public async Task<ActionResult<CustomerGet>> GetList()
    {
        return Ok(_customerListMapper.ToDto(await _requestBus.Send(new GetCustomerListQuery())));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<CustomerGet>>> GetById(int id)
    {
        return Ok(_customerMapper.ToDto(await _requestBus.Send(new GetCustomerByIdQuery(id))));
    }

    // todo
}
