using SharedResources.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdministartionWebsite.Infrastructure.Profiles;

var builder = WebApplication.CreateBuilder(args);

var (_services, _config) = (builder.Services, builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
_services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString)
    )
    .AddDatabaseDeveloperPageExceptionFilter();

_services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
_services.AddControllersWithViews();

_services
    .AddAutoMapper(typeof(ProjectProfile))
    .AddAutoMapper(typeof(CustomerProfile))
    ;

var app = builder.Build();

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
