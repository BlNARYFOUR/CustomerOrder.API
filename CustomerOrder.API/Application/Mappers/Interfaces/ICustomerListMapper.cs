using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface ICustomerListMapper
{
    public IEnumerable<Dtos.CustomerGet> ToDto(IEnumerable<Domain.Entities.Customer> entities);
}
