namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringPostDTO
{
    public string? Beskrivning { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
    public int ProjectId { get; set; }
}