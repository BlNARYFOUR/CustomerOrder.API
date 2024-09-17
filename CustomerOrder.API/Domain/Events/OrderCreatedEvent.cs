using MediatR;

namespace CustomerOrder.API.Domain.Events;

public record OrderCreatedEvent(int Id, int CustomerId) : INotification;
