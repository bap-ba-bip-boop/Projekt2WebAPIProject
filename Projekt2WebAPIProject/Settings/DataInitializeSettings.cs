namespace WebAPI.Settings;

public class DataInitializeSettings
{
    public List<CusotmerInit>? CustomersToAdd { get; set; }
    public List<ProjectInit>? ProjectsToAdd { get; set; }
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