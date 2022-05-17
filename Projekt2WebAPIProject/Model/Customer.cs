using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model;

public class Customer
{

    [Key]
    public int CustomerId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
}
