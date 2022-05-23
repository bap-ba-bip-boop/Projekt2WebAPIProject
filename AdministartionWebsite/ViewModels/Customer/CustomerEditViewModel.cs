using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerEditViewModel
{
    public int CustomerId { get; set; }
    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }
}
