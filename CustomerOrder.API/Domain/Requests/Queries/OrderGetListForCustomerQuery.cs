using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record OrderGetListForCustomerQuery(int CustomerId) : IRequest<IEnumerable<Order>>;
