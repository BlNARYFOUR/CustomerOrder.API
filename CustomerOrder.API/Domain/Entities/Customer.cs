using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerOrder.API.Domain.Entities;

public class Customer(string firstName, string lastName, string email)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = firstName;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = lastName;
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = email;
    [Required]
    public int NumberOfOrders { get; set; } = 0;

    public ICollection<Order> Orders { get; set; } = [];
}
