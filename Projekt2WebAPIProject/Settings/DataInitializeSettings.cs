namespace WebAPI.Settings;

public class DataInitializeSettings
{
    public List<CusotmerInit>? CustomersToAdd { get; set; }
}

public class CusotmerInit
{
    public string? Name { get; set; }
}