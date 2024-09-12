using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record OrderGetListForCustomerQuery(int CustomerId, string? From, string? To) : IRequest<IEnumerable<Order>>;
