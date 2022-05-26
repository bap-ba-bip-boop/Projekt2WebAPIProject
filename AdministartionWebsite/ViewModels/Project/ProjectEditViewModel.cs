using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectEditViewModel
{
    public int ProjectId { get; set; }
    [Required(ErrorMessage = "The Project must have a name")]
    [MaxLength(30, ErrorMessage = "Can't be longer than 30 characters")]
    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }
}