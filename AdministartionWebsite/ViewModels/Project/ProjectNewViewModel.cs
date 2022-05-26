using System.ComponentModel.DataAnnotations;
using AdministartionWebsite.Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectNewViewModel
{
    [Required(ErrorMessage = "The Project must have a name")]
    [MaxLength(30, ErrorMessage = "Can't be longer than 30 characters")]
    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }

    [Required(ErrorMessage = "Must select a Project owner")]
    [ValidId(ErrorMessage = "Must select a Project owner")]
    [Display(Name = "Project Owner")]
    public int CustomerId { get; set; }
    public List<SelectListItem>? CustomerList { get; set; }
}