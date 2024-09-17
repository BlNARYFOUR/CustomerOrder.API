using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Events;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Services;
using MediatR;

namespace CustomerOrder.API.Domain.EventHandlers;

public class SendOrderCreatedEmailHandler(
    IMailer mailer,
    ICustomerRepository customerRepository,
    IEmailRepository emailRepository
) : INotificationHandler<OrderCreatedEvent> {
    private readonly IMailer _mailer = mailer ?? throw new ArgumentNullException(nameof(mailer));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IEmailRepository _emailRepository = emailRepository ?? throw new ArgumentNullException(nameof(emailRepository));

    /// <exception cref="Exceptions.NotFoundException" />
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(notification.CustomerId);

        var email = new Email(
            "noreply@test.test",
            customer.Email,
            "Order Confirmation",
            $"Hi {customer.FirstName},\n\nYour order #{notification.Id} is on its way!\n\nKind regards,\n\nThe CustomerOrder Team"
        );

        var token = _mailer.Send(email);
        email.Token = token;
        await _emailRepository.CreateAsync(email);
    }
}
