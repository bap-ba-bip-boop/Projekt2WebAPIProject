using Microsoft.EntityFrameworkCore;
using SharedResources.Services;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Model;
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

_services.AddDbContext<APIDbContext>(options =>
    options.UseSqlServer(connectionString)
);

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
    .AddAutoMapper(typeof(CustomerProfile))
    .AddAutoMapper(typeof(ProjectProfile));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
