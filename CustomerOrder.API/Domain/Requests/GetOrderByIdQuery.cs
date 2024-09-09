using CustomerOrder.API.Domain.Entities;
using MediatR;

namespace CustomerOrder.API.Domain.Requests;

public record GetOrderByIdQuery(int Id) : IRequest<Order>;
