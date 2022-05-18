using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model;

public class TidsRegistrering
{
    [Key]
    public int TidsRegistreringId { get; set; }
    [Required]
    public DateTime Datum { get; set; }
    [MaxLength(100)]
    public string? Beskrivning { get; set; }
    [Range(1,1440)]
    [Required]
    public int AntalMinuter { get; set; }
    [Required]
    public int ProjectId { get; set; }
    public Project? Project{ get; set; }
}