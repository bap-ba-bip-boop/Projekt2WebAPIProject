using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Customer;

public class CustomerEditViewModel
{
    public int CustomerId { get; set; }
    [Required]
    [MaxLength(30)]
    [Display(Name = "Customer Name")]
    public string? CustomerName { get; set; }
}
