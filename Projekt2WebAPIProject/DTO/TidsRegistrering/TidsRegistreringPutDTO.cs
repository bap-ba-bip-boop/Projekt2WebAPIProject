namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringPutDTO
{
    public string? Beskrivning { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
}