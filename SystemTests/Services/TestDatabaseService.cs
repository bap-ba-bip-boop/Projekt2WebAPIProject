using System;
using Microsoft.EntityFrameworkCore;
using SharedResources.Services;
using WebAPI.Model;
using static SharedResources.Services.ICreateUniqeService;

namespace SystemTests.Services;

class TestDatabaseService
{
    public static APIDbContext CreateTestContext(string dbName) =>
        new APIDbContext(
            new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options
        );
    public static CreateUniqueStatus AddCustomer(string name, CreateUniqeService _creator, APIDbContext _context) =>
        _creator.CreateIfNotExists(
            _context,
            _context.Customers!,
            item => item.Name!.Equals(name),
            new Customer
            {
                Name = name
            }
        );
    public static CreateUniqueStatus AddProject(string name, int customerId, CreateUniqeService _creator, APIDbContext _context) =>
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
        APIDbContext _context) =>
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