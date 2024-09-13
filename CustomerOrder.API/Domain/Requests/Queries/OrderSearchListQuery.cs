using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record OrderSearchListQuery(List<int> CustomerIds, string? From, string? To) : IRequest<IEnumerable<Order>>;
