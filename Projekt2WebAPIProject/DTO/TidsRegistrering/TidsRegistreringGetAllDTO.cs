namespace WebAPI.DTO.TidsRegistrering;

public class TidsRegistreringGetAllDTO
{
    public int TidsRegistreringId { get; set; }
    public string? ProjectName { get; set; }
    public DateTime Datum { get; set; }
    public int AntalMinuter { get; set; }
}