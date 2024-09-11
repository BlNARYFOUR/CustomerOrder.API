using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record OrderGetCancelledListForCustomerQuery(int CustomerId) : IRequest<IEnumerable<Order>>;
