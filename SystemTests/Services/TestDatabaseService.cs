using System;
using Microsoft.EntityFrameworkCore;
using SharedResources.Data;
using SharedResources.Services;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.Services;

class TestDatabaseService
{
    public static ApplicationDbContext CreateTestContext(string dbName) =>
        new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options
        );
    public static CreateUniqueStatus AddCustomer(string name, CreateUniqeService _creator, ApplicationDbContext _context) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.CustomerName!.Equals(name),
            new Customer
            {
                CustomerName = name
            }
        );
    public static CreateUniqueStatus AddProject(string name, int customerId, CreateUniqeService _creator, ApplicationDbContext _context) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Projects!,
            item => item.ProjectName!.Equals(name),
            new Project
            {
                ProjectName = name,
                CustomerId = customerId
            }
        );
    public static CreateUniqueStatus AddTidsRegistrering(
        string Beskrivning,
        int AntalMinuter,
        int ProjectId,
        DateTime dt,
        CreateUniqeService _creator,
        ApplicationDbContext _context) =>
        _creator.CreateIfNotExists(
            _context,
            _context.TidsRegistrerings!,
            item => false,
            new TidsRegistrering
            {
                Datum = dt,
                Beskrivning = Beskrivning,
                AntalMinuter = AntalMinuter,
                ProjectId = ProjectId
            }
        );
}