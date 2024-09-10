using CustomerOrder.API.Application.Dtos;
using CustomerOrder.API.Application.Mappers.Interfaces;
using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Application.Mappers;

public class OrderListMapper(IOrderMapper mapper) : IOrderListMapper
{
    private readonly IOrderMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public IEnumerable<OrderGet> ToDto(IEnumerable<Order> entities)
    {
        var dtos = new List<OrderGet>();

        foreach (var entity in entities)
        {
            dtos.Add(_mapper.ToDto(entity));
        }

        return dtos;
    }
}
