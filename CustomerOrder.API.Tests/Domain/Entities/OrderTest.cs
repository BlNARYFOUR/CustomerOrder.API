using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class OrderTest
{
    private readonly Order _entity;

    public OrderTest()
    {
        _entity = new Order(2, "description", 1.99);
    }

    [Fact]
    public void ItHasAnIdTest()
    {
        Assert.Equal(0, _entity.Id);
    }

    [Fact]
    public void ItHasADescriptionTest()
    {
        Assert.Same("description", _entity.Description);
    }

    [Fact]
    public void ItHasAPriceTest()
    {
        Assert.Equal(1.99, _entity.Price);
    }

    [Fact]
    public void ItHasACreationDateTest()
    {
        Assert.IsType<DateTime>(_entity.CreationDate);
    }

    [Fact]
    public void ItHasACustomerIdTest()
    {
        Assert.Equal(2, _entity.CustomerId);
    }

    [Fact]
    public void ItHasACustomerTest()
    {
        Assert.Null(_entity.Customer);
    }

    [Fact]
    public void ItHasAStatusTest()
    {
        Assert.Equal(OrderStatus.CREATED, _entity.Status);
    }

    [Fact]
    public void ItCanSetAnIdTest()
    {
        _entity.Id = 2;
        Assert.Equal(2, _entity.Id);
    }

    [Fact]
    public void ItCanSetADescriptionTest()
    {
        _entity.Description = "description_2";
        Assert.Same("description_2", _entity.Description);
    }

    [Fact]
    public void ItCanSetAPriceTest()
    {
        _entity.Price = 2.99;
        Assert.Equal(2.99, _entity.Price);
    }

    [Fact]
    public void ItCanSetACreationDateTest()
    {
        _entity.CreationDate = new DateTime(1999, 1, 1);
        Assert.Equal(new DateTime(1999, 1, 1), _entity.CreationDate);
    }

    [Fact]
    public void ItCanSetACustomerIdTest()
    {
        _entity.CustomerId = 3;
        Assert.Equal(3, _entity.CustomerId);
    }

    [Fact]
    public void ItCanSetACustomerTest()
    {
        var expected = new Customer("", "", "");
        _entity.Customer = expected;
        Assert.Equal(expected, _entity.Customer);
    }

    [Fact]
    public void ItCanSetAStatusTest()
    {
        _entity.Status = OrderStatus.CANCELLED;
        Assert.Equal(OrderStatus.CANCELLED, _entity.Status);
    }
}
