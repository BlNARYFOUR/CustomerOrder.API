namespace CustomerOrder.API.Application.Services.Mappers;

public interface ICustomerListMapper
{
    public IEnumerable<Domain.Entities.Customer> FromDto(IEnumerable<Dtos.Customer> customerDtos);
    public IEnumerable<Dtos.Customer> ToDto(IEnumerable<Domain.Entities.Customer> customers);
}
