using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedResources.Services;
using WebAPI.Settings;

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
        SeedCustomers();
        SeedProjects();
        SeedTidsRegistrering();
    }

    private void SeedTidsRegistrering()
    {
        _settings.Value.TidsRegistreringToAdd!.ForEach(newReg =>
            _creator.CreateIfNotExists(
                _context,
                _context.TidsRegistrerings!,
                reg => reg.Beskrivning == newReg.Beskrivning,
                new TidsRegistrering
                {
                    Datum = DateTime.Parse(newReg.Datum!),
                    Beskrivning = newReg.Beskrivning,
                    AntalMinuter = newReg.AntalMinuter,
                    ProjectId = _context.Projects!.ToList().ElementAt(newReg.ProjectId - 1).ProjectId,
                    Project = _context.Projects!.ToList().ElementAt(newReg.ProjectId - 1)
                }
            )
        );
    }

    private void SeedProjects()
    {
        _settings.Value.ProjectsToAdd!.ForEach(project =>
            _creator.CreateIfNotExists(
                _context,
                _context.Projects!,
                proj => proj.ProjectName!.Equals(project.ProjectName),
                new Project
                {
                    ProjectName = project.ProjectName,
                    CustomerId = _context.Customers!.ToList().ElementAt(project.CustomerId - 1).CustomerId,
                    Customer = _context.Customers!.ToList().ElementAt(project.CustomerId - 1)
                }
            )
        );
    }
    private void SeedCustomers()
    {
        _settings.Value.CustomersToAdd!.ForEach(customer =>
            _creator.CreateIfNotExists(
                _context,
                _context.Customers!,
                cust => cust.Name!.Equals(customer.CustomerName),
                new Customer
                {
                    Name = customer.CustomerName
                }
            )
        );
    }
}
