using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model;

public class Project
{
    [Key]
    public int ProjectId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [MaxLength(30)]
    [Required]
    public string? ProjectName { get; set; }
}