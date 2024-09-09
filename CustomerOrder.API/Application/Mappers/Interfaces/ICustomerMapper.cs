using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface ICustomerMapper
{
    public Customer FromDto(CustomerUpsert dto);
    public CustomerGet ToDto(Customer entity);
}
