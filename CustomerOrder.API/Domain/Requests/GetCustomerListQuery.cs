using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public record GetCustomerListQuery() : IRequest<IEnumerable<Customer>>;
