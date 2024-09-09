using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public record GetCustomerByIdQuery(int Id) : IRequest<Customer>;
