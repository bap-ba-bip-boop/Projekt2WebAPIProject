using System.ComponentModel.DataAnnotations;
using AdministartionWebsite.Infrastructure.Validation;

namespace WebAPI.DTO.Project;

public class ProjectPostDTO
{
    [Required(ErrorMessage = "The Project must have a name")]
    [MaxLength(30, ErrorMessage = "Can't be longer than 30 characters")]
    public string? ProjectName { get; set; }
    [Required(ErrorMessage = "Must select a Project owner")]
    [ValidId(ErrorMessage = "Must select a valid Project owner")]
    public int CustomerId { get; set; }
}