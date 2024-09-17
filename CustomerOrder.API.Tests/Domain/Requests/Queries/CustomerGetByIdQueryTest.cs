using CustomerOrder.API.Domain.Entities;
using CustomerOrder.API.Domain.Requests.Queries;
using MediatR;

namespace CustomerOrder.API.Tests.Domain.Requests.Queries;

public class CustomerGetByIdQueryTest
{
    private readonly CustomerGetByIdQuery _command;

    public CustomerGetByIdQueryTest()
    {
        _command = new CustomerGetByIdQuery(1234);
    }

    [Fact]
    public void ItIsAQueryTest()
    {
        Assert.IsAssignableFrom<IRequest<Customer>>(_command);
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(1234, _command.Id);
    }
}
