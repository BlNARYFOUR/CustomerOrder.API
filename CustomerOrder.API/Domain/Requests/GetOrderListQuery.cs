using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public record GetOrderListQuery() : IRequest<IEnumerable<Order>>;
