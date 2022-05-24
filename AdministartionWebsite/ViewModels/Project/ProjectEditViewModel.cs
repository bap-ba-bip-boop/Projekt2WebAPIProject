using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectEditViewModel
{
    public int ProjectId { get; set; }
    [MaxLength(30)]
    [Required]
    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }
}