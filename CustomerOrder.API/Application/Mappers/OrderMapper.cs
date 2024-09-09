
using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers;

public class OrderMapper : IOrderMapper
{
    public Order FromDto(OrderUpsert dto)
    {
        return new Order(
            dto.CustomerId,
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
            entity.CreationDate
        );
    }
}
