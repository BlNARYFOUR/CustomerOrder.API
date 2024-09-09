namespace CustomerOrder.API.Application.Services.Mappers;

public class CustomerListMapper(ICustomerMapper mapper) : ICustomerListMapper
{
    private readonly ICustomerMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public IEnumerable<Domain.Entities.Customer> FromDto(IEnumerable<Dtos.Customer> customerDtos)
    {
        var customers = new List<Domain.Entities.Customer>();

        foreach (var customerDto in customerDtos)
        {
            customers.Add(_mapper.FromDto(customerDto));
        }

        return customers;
    }

    public IEnumerable<Dtos.Customer> ToDto(IEnumerable<Domain.Entities.Customer> customers)
    {
        var customerDtos = new List<Dtos.Customer>();

        foreach (var customer in customers)
        {
            customerDtos.Add(_mapper.ToDto(customer));
        }

        return customerDtos;
    }
}
