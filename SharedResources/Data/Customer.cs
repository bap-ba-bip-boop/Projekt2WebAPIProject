using System.ComponentModel.DataAnnotations;

namespace SharedResources.Data;

public class Customer
{

    [Key]
    public int CustomerId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? CustomerName { get; set; }

    public List<Project>? Projects { get; set; }
}
