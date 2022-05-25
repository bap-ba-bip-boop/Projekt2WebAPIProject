using System.ComponentModel.DataAnnotations;
using AdministartionWebsite.Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectNewViewModel
{
    [MaxLength(30)]
    [Required]
    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }

    [Required]
    [Display(Name = "Project Owner")]
    [ValidId]
    public int CustomerId { get; set; }
    public List<SelectListItem>? CustomerList { get; set; }
}