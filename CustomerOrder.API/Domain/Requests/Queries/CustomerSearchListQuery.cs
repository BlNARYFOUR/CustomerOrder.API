using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record CustomerSearchListQuery(string? EmailSearch) : IRequest<IEnumerable<Customer>>;
