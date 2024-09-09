using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Services.Mappers;
using CustomerOrder.API.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerOrder.API.Application.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController(
    ICustomerRepository repository,
    ICustomerMapper mapper,
    ICustomerListMapper listMapper
) : ControllerBase {
    private readonly ICustomerRepository _customerRepository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly ICustomerMapper _customerMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICustomerListMapper _customerListMapper = listMapper ?? throw new ArgumentNullException(nameof(listMapper));

    [HttpGet]
    public async Task<ActionResult<Customer>> GetList()
    {
        return Ok(_customerListMapper.ToDto(await _customerRepository.GetAllAsync()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetById(int id)
    {
        return Ok(_customerMapper.ToDto(await _customerRepository.GetByIdAsync(id)));
    }

    // todo
}
