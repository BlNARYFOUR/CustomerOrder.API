using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.Queries;

public record OrderGetByIdQuery(int Id) : IRequest<Order>;
