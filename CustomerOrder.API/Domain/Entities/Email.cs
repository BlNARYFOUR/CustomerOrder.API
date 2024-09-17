using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerOrder.API.Domain.Entities;

public class Email(string from, string to, string subject, string message)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Token { get; set; } = "";
    [Required]
    [MaxLength(50)]
    public string From { get; set; } = from;
    [Required]
    [MaxLength(50)]
    public string To { get; set; } = to;
    [Required]
    [MaxLength(64)]
    public string Subject { get; set; } = subject;
    [Required]
    [MaxLength(256)]
    public string Message { get; set; } = message;
}
