using System.ComponentModel.DataAnnotations;

namespace AdministartionWebsite.ViewModels.Project;

public class ProjectEditViewModel
{
    public int ProjectId { get; set; }
    [MaxLength(30)]
    [Required]
    public string? ProjectName { get; set; }
}