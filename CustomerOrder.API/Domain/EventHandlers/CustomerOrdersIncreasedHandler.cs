﻿using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Repositories;
using MediatR;

namespace CustomerOrder.API.Domain.EventHandlers
{
    public class CustomerOrdersIncreasedHandler(ICustomerRepository repository) : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _repository.IncreaseNumberOfOrdersAsync(notification.CustomerId);
        }
    }
}
