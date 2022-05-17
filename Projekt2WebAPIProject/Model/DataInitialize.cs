using Microsoft.Extensions.Options;
using SharedResources.Services;
using WebAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model;

public class DataInitialize
{
    private readonly APIDbContext _context;
    private readonly IOptions<DataInitializeSettings> _settings;
    private readonly ICreateUniqeService _creator;

    public DataInitialize(APIDbContext context, IOptions<DataInitializeSettings> settings, ICreateUniqeService creator)
    {
        _context = context;
        _settings = settings;
        _creator = creator;
    }

    public void SeedData()
    {
        _context.Database.Migrate();

        _settings.Value.CustomersToAdd!.ForEach(customer =>
            _creator.CreateIfNotExists(
                _context,
                _context.Customers!,
                cust => cust.Name!.Equals(customer.Name),
                new Customer
                {
                    Name = customer.Name
                }
            )
        );

    }
}
