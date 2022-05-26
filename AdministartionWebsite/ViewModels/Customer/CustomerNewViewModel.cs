using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerNewViewModel
{
    [Required(ErrorMessage = "The Customer must have a name")]
    [MaxLength(30, ErrorMessage = "Can't be longer than 30 characters")]
    [Display(Name = "Customer Name")]
    public string? CustomerName { get; set; }
}
