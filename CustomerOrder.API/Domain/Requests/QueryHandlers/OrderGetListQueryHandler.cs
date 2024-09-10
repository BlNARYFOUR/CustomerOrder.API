﻿using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Domain.Requests.QueryHandlers;

public class OrderGetListQueryHandler(IOrderRepository repository) : IRequestHandler<OrderGetListQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<Order>> Handle(OrderGetListQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
