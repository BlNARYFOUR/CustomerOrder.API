namespace CustomerOrder.API.Application.Dtos;

public class CustomerUpsert(
    string firstName,
    string lastName,
    string email
) {
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Email { get; } = email;
}
