using AdministartionWebsite.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedResources.Data;

namespace AdministartionWebsite.Data;

public class DataInitialize{
    private readonly ApplicationDbContext _context;
    private readonly IOptions<DataInitializeSettings> _settings;
    private readonly UserManager<IdentityUser> _userManager;

    public DataInitialize(ApplicationDbContext context, IOptions<DataInitializeSettings> settings, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _settings = settings;
        _userManager = userManager;
    }
    public void SeedData()
    {
        SeedUsers();
    }
    private void SeedUsers()
    {
        _settings.Value.UsersToAdd!.ForEach(newUser =>
            CreateUserIfNotExists(newUser.Email!, newUser.Password!)
        ); ;
    }
    private void CreateUserIfNotExists(string email, string password)
    {
        if (_userManager.FindByEmailAsync(email).Result != null) return;

        var user = new IdentityUser
        {
            Email = email,
            UserName = email,
            EmailConfirmed = true
        };

        _userManager.CreateAsync(user, password).Wait();
    }
}