
using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers;

public class OrderMapper : IOrderMapper
{
    public Order FromDto(int customerId, OrderUpsert dto)
    {
        return new Order(
            customerId,
            dto.Description,
            dto.Price
        );
    }

    public OrderGet ToDto(Order entity)
    {
        return new OrderGet(
            entity.Id,
            entity.CustomerId,
            entity.Description,
            entity.Price,
            entity.CreationDate.ToUniversalTime(),
            entity.Status.ToString()
        );
    }
}
