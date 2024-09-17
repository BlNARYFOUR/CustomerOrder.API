using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Exceptions;
using CustomerOrder.API.Domain.Models;
using CustomerOrder.API.Domain.Repositories;
using CustomerOrder.API.Domain.Requests.CommandHandlers;
using CustomerOrder.API.Domain.Requests.Commands;
using MediatR;
using Moq;

namespace CustomerOrder.API.Tests.Domain.Requests.CommandHandlers;

public class CustomerUpdateCommandHandlerTest
{
    private readonly Mock<ICustomerRepository> _repositoryMock;

    private readonly CustomerUpdateCommandHandler _commandHandler;

    public CustomerUpdateCommandHandlerTest()
    {
        _repositoryMock = new Mock<ICustomerRepository>();

        _commandHandler = new CustomerUpdateCommandHandler(
            _repositoryMock.Object
        );
    }

    [Fact]
    public void ItIsACommandHandlerTest()
    {
        Assert.IsAssignableFrom<IRequestHandler<CustomerUpdateCommand>>(_commandHandler);
    }

    [Fact]
    public async Task ItCanHandleTheCommandTest()
    {
        var expectedCommand = new CustomerUpdateCommand(1234, "test_first_name_updated", "test_last_name_updated", "test_email_updated");
        var expectedCustomer = new Customer(expectedCommand.FirstName, expectedCommand.LastName, expectedCommand.Email) { Id = expectedCommand.Id };
        var customer = new Customer("test_first_name_original", "test_last_name_original", "test_email_original") { Id = expectedCustomer.Id };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult(customer));
        _repositoryMock.Setup(r => r.FindByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<Customer?>(null));
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Customer>()))
            .Returns(Task.FromResult(expectedCustomer));

        await _commandHandler.Handle(expectedCommand, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedCommand.Id), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(expectedCommand.Email), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Customer>(
            c => expectedCustomer.Id == c.Id
                && expectedCustomer.FirstName == c.FirstName
                && expectedCustomer.LastName == c.LastName
                && expectedCustomer.Email == c.Email
                && expectedCustomer.NumberOfOrders == c.NumberOfOrders
                && expectedCustomer.Orders.Count == c.Orders.Count
        )), Times.Once);
    }

    [Fact]
    public async Task ItPropegatesNotFoundExceptionTest()
    {
        var expectedCommand = new CustomerUpdateCommand(1234, "test_first_name_updated", "test_last_name_updated", "test_email_updated");
        var expectedException = NotFoundException.ForClass("TestClass");
        
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .Throws(expectedException);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _commandHandler.Handle(expectedCommand, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedCommand.Id), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Customer>()), Times.Never);

        Assert.Equal(expectedException.Message, exception.Message);
    }

    [Fact]
    public async Task ItThrowsAValidationExceptionOnDuplicateEmailTest()
    {
        var expectedCommand = new CustomerUpdateCommand(1234, "test_first_name_updated", "test_last_name_updated", "test_email_updated");
        var customer = new Customer("test_first_name_original", "test_last_name_original", "test_email_original") { Id = expectedCommand.Id };
        var customerWithEmail = new Customer(expectedCommand.FirstName, expectedCommand.LastName, expectedCommand.Email) { Id = 4321 };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
             .Returns(Task.FromResult(customer));
        _repositoryMock.Setup(r => r.FindByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<Customer?>(customerWithEmail));

        var exception = await Assert.ThrowsAsync<ValidationException>(async () => {
            await _commandHandler.Handle(expectedCommand, CancellationToken.None);
        });

        _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(r => r.GetByIdAsync(expectedCommand.Id), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _repositoryMock.Verify(r => r.FindByEmailAsync(expectedCommand.Email), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Customer>()), Times.Never);

        Assert.Equal([new ValidationError("Email", "'Email' is already used.")], exception.Errors);
    }
}
