using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringPostDTO
{
    [MaxLength(100)]
    public string? Beskrivning { get; set; }
    [Required]
    public DateTime Datum { get; set; }
    [Range(1, 1440)]
    [Required]
    public int AntalMinuter { get; set; }
    [Required]
    public int ProjectId { get; set; }
}