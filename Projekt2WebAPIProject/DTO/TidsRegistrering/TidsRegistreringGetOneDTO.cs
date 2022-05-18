namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringGetOneDTO
{
    public string? CustomerName { get; set; }
    public string? ProjectName { get; set; }
    public string? Beskrivning { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
}