using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface IOrderListMapper
{
    public IEnumerable<OrderGet> ToDto(IEnumerable<Order> entities);
}
