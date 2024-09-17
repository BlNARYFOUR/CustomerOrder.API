using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers.Interfaces;

public interface IOrderMapper
{
    public OrderGet ToDto(Order entity);
}
