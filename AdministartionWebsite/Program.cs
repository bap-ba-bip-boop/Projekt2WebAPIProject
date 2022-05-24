using SharedResources.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdministartionWebsite.Infrastructure.Profiles;
using AdministartionWebsite.Settings;
using AdministartionWebsite.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();;

var (_services, _config) = (builder.Services, builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
_services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString)
    )
    .AddDatabaseDeveloperPageExceptionFilter();

_services.AddDefaultIdentity<IdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = true
    )
    .AddEntityFrameworkStores<ApplicationDbContext>();

_services.
    Configure<DataInitializeSettings>(
        _config.GetSection(nameof(DataInitializeSettings))
    );

_services.AddControllersWithViews();

_services
    .AddTransient<DataInitialize>();

//needs to be in the order of TimeReg => Project => Customer because of dependancies
_services
    .AddAutoMapper(typeof(TimeRegProfile))
    .AddAutoMapper(typeof(ProjectProfile))
    .AddAutoMapper(typeof(CustomerProfile))
    ;

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitialize>()!.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
