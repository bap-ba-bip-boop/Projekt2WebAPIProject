using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerNewViewModel
{
    [Required]
    [MaxLength(30)]
    [Display(Name = "Customer Name")]
    public string? CustomerName { get; set; }
}
