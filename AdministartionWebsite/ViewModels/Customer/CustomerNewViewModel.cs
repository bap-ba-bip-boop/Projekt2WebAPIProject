using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerNewViewModel
{
    [Required]
    [MaxLength(30)]
    public string? CustomerName { get; set; }
}
