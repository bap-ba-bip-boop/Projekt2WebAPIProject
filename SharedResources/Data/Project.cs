using System.ComponentModel.DataAnnotations;

namespace SharedResources.Data;

public class Project
{
    [Key]
    public int ProjectId { get; set; }
    [MaxLength(30)]
    [Required]
    public string? ProjectName { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}