namespace WebAPI.Settings;

public class DataInitializeSettings
{
    public List<CusotmerInit>? CustomersToAdd { get; set; }
    public List<ProjectInit>? ProjectsToAdd { get; set; }
    public List<TidsRegistreringInit>? TidsRegistreringToAdd { get; set; }
}

public class CusotmerInit
{
    public string? CustomerName { get; set; }
}

public class ProjectInit
{
    public string? ProjectName { get; set; }
    public int CustomerId { get; set; }
}

public class TidsRegistreringInit
{
    public string? Datum { get; set; }
    public string? Beskrivning { get; set; }
    public int AntalMinuter { get; set; }
    public int ProjectId { get; set; }
}