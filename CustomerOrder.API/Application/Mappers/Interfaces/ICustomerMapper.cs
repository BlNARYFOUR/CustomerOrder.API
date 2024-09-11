using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface ICustomerMapper
{
    public Dtos.CustomerGet ToDto(Domain.Entities.Customer entity);
}
