using CustomerOrder.API.Domain.Entities;

namespace CustomerOrder.API.Tests.Domain.Entities;

public class OrderStatusTest
{
    private readonly OrderStatus _enum;

    public OrderStatusTest()
    {
        _enum = new OrderStatus();
    }

    [Fact]
    public void ItIsAValidEnumTest()
    {
        Assert.IsAssignableFrom<Enum>(_enum);
    }

    [Theory]
    [MemberData(nameof(EnumData))]
    public void ItHasCorrectValuesTest((int Expected, OrderStatus Enum) data)
    {
        Assert.Equal(data.Expected, (int) data.Enum);
    }

    public static IEnumerable<object[]> EnumData()
    {
        yield return new object[] { (0, OrderStatus.CREATED) };
        yield return new object[] { (1, OrderStatus.CANCELLED) };
    }
}
