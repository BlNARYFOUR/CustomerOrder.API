namespace CustomerOrder.API.Application.Dtos;

public class CustomerUpsert(
    string firstName,
    string lastName,
    string email
) {
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
}
