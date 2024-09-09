namespace CustomerOrder.API.Application.Services.Mappers;

public interface ICustomerMapper
{
    public Domain.Entities.Customer FromDto(Dtos.Customer customerDto);
    public Dtos.Customer ToDto(Domain.Entities.Customer customer);
}
