using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface ICustomerListMapper
{
    public IEnumerable<Customer> FromDto(IEnumerable<CustomerUpsert> dtos);
    public IEnumerable<CustomerGet> ToDto(IEnumerable<Customer> entities);
}
