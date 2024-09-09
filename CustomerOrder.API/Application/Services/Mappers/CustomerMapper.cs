namespace CustomerOrder.API.Application.Services.Mappers;

public class CustomerMapper : ICustomerMapper
{
    public Domain.Entities.Customer FromDto(Dtos.Customer customerDto)
    {
        return new Domain.Entities.Customer(
            customerDto.FirstName,
            customerDto.LastName,
            customerDto.Email,
            customerDto.NumberOfOrders
        )
        { Id = customerDto.Id };
    }

    public Dtos.Customer ToDto(Domain.Entities.Customer customer)
    {
        return new Dtos.Customer(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Email,
            customer.NumberOfOrders
        );
    }
}
