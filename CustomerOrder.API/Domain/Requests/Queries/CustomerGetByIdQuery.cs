using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record CustomerGetByIdQuery(int Id) : IRequest<Customer>;
