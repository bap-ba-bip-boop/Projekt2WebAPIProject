using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;
using SharedResources.Services;
using WebAPI.Data;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Services;
using WebAPI.Settings;

var builder = WebApplication.CreateBuilder(args);

var (_services, _config) = (builder.Services, builder.Configuration);
var connectionString = _config.GetConnectionString("DefaultConnection");

// Add services to the container.

_services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_services.AddEndpointsApiExplorer();
_services.AddSwaggerGen();

_services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

_services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

_services.
    Configure<DataInitializeSettings>(
        _config.GetSection(nameof(DataInitializeSettings))
    );

_services
    .AddTransient<DataInitialize>()
    .AddTransient<ICreateUniqeService, CreateUniqeService>()
    .AddTransient<IAPIMethodWrapperService, APIMethodWrapperService>()
    .AddTransient<IDbLookupService, DbLookupService>();

_services
    .AddAutoMapper(typeof(ProjectProfile))
    .AddAutoMapper(typeof(TidsRegistreringProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitialize>()!.SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors( config =>
    config
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials() // allow credentials
);

app.UseAuthorization();

app.MapControllers();

app.Run();
