using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers;

public class CustomerMapper : ICustomerMapper
{
    public Customer FromDto(CustomerUpsert dto)
    {
        return new Customer(
            dto.FirstName,
            dto.LastName,
            dto.Email,
            dto.NumberOfOrders
        );
    }

    public CustomerGet ToDto(Customer entity)
    {
        return new CustomerGet(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.NumberOfOrders
        );
    }
}
