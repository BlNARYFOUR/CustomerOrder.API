using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers;

public class CustomerListMapper(ICustomerMapper mapper) : ICustomerListMapper
{
    private readonly ICustomerMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public IEnumerable<CustomerGet> ToDto(IEnumerable<Customer> entities)
    {
        var customerDtos = new List<CustomerGet>();

        foreach (var entity in entities)
        {
            customerDtos.Add(_mapper.ToDto(entity));
        }

        return customerDtos;
    }
}
