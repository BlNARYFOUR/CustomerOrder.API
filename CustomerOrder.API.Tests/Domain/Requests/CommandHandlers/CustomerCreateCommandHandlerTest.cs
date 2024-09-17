using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Models;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.CommandHandlers;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.CommandHandlers;

public class CustomerCreateCommandHandlerTest
{
    private readonly Mock<ICustomerRepository> _repositoryMock;

    private readonly CustomerCreateCommandHandler _commandHandler;

    public CustomerCreateCommandHandlerTest()
    {
        _repositoryMock = new Mock<ICustomerRepository>();

        _commandHandler = new CustomerCreateCommandHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsACommandHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<CustomerCreateCommand, int>>(_commandHandler);
    }

    [Fact]
    public async Task ItCanHandleTheCommandTest()
    {
        var expectedCommand = new CustomerCreateCommand("test_first_name", "test_last_name", "test_email");
        var expectedCustomer = new Customer(expectedCommand.FirstName, expectedCommand.LastName, expectedCommand.Email);
        var customerWithId = new Customer(expectedCommand.FirstName, expectedCommand.LastName, expectedCommand.Email) { Id = 1234 };

        _repositoryMock.Setup(r => r.FindByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<Customer?>(null));
        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Customer>()))
            .Returns(Task.FromResult(customerWithId));

        int result = await _commandHandler.Handle(expectedCommand, CancellationToken.None);

        _repositoryMock.Verify(r => r.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(expectedCommand.Email), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Customer>()), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<Customer>(
            c => expectedCustomer.Id == c.Id
                && expectedCustomer.FirstName == c.FirstName
                && expectedCustomer.LastName == c.LastName
                && expectedCustomer.Email == c.Email
                && expectedCustomer.NumberOfOrders == c.NumberOfOrders
                && expectedCustomer.Orders.Count == c.Orders.Count
        )), Times.Once);

        Assert.Equal(customerWithId.Id, result);
    }

    [Fact]
    public async Task ItThrowsAValidationExceptionOnDuplicateEmailTest()
    {
        var expectedCommand = new CustomerCreateCommand("test_first_name", "test_last_name", "test_email");
        var customerWithId = new Customer(expectedCommand.FirstName, expectedCommand.LastName, expectedCommand.Email) { Id = 1234 };

        _repositoryMock.Setup(r => r.FindByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<Customer?>(customerWithId));

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => {
            await _commandHandler.Handle(expectedCommand, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(expectedCommand.Email), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Customer>()), Times.Never);

        Assert.Equal([new ValidationError("Email", "'Email' is already used.")], exception.Errors);
    }
}
