using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectNewViewModel
{
    [MaxLength(30)]
    [Required]
    public string? ProjectName { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public List<SelectListItem>? CustomerList { get; set; }
}