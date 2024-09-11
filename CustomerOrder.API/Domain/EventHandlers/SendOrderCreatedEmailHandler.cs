﻿using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Services;
using MediatR;

namespace CustomerOrder.API.Domain.EventHandlers;

public class SendOrderCreatedEmail(IMailer mailer, ICustomerRepository repository) : INotificationHandler<OrderCreatedEvent>
{
    private readonly IMailer _mailer = mailer ?? throw new ArgumentNullException(nameof(mailer));
    private readonly ICustomerRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(notification.CustomerId);

        var email = new Email(
            "noreply@test.test",
            customer.Email,
            "Order Confirmation",
            $"Hi {customer.FirstName},\n\nYour order #{notification.Id} is on its way!\n\nKind regards,\n\nThe CustomerOrder Team"
        );

        var token = _mailer.Send(email);

        email.Token = token;

        // save email to DB & provide a retry webhook
    }
}
