using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class CustomerTest
{
    private readonly Customer _entity;

    public CustomerTest()
    {
        _entity = new Customer("first_name", "last_name", "email");
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _entity.Id);
    }

    [Fact]
    public void ItHasAFirstNameTest()
    {
        Assert.Equal("first_name", _entity.FirstName);
    }

    [Fact]
    public void ItHasALastNameTest()
    {
        Assert.Equal("last_name", _entity.LastName);
    }

    [Fact]
    public void ItHasAnEmailTest()
    {
        Assert.Equal("email", _entity.Email);
    }

    [Fact]
    public void ItHasANumberOfOrdersTest()
    {
        Assert.Equal(0, _entity.NumberOfOrders);
    }

    [Fact]
    public void ItHasOrdersTest()
    {
        Assert.Equal([], _entity.Orders);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _entity.Id = 2;
        Assert.Equal(2, _entity.Id);
    }

    [Fact]
    public void ItCanSetAFirstNameTest()
    {
        _entity.FirstName = "first_name_2";
        Assert.Equal("first_name_2", _entity.FirstName);
    }

    [Fact]
    public void ItCanSetALastNameTest()
    {
        _entity.LastName = "last_name_2";
        Assert.Equal("last_name_2", _entity.LastName);
    }

    [Fact]
    public void ItCanSetAnEmailTest()
    {
        _entity.Email = "email_2";
        Assert.Equal("email_2", _entity.Email);
    }

    [Fact]
    public void ItCanSetANumberOfOrdersTest()
    {
        _entity.NumberOfOrders = 2;
        Assert.Equal(2, _entity.NumberOfOrders);
    }

    [Fact]
    public void ItCanSetOrdersTest()
    {
        ICollection<Order> expected = [new Order(1, "description", 1.23)];
        _entity.Orders = expected;
        Assert.Equal(expected, _entity.Orders);
    }
}
