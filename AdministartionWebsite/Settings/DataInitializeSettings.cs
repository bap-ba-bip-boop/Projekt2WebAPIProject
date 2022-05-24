namespace AdministartionWebsite.Settings;

public class DataInitializeSettings
{
    public List<UserInit>? UsersToAdd { get; set; }
}

public class UserInit
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}