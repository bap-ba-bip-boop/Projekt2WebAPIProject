namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringGetAllDTO
{
    public string? ProjectName { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
}