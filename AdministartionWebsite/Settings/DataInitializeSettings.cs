namespace AdministartionWebsite.Settings;

public class DataInitializeSettings
{
    public List<UserInit>? UsersToAdd { get; set; }
}

public class UserInit
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}