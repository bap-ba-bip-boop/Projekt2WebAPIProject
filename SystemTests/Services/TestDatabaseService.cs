using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace SystemTests.Services;

class TestDatabaseService
{
    public static APIDbContext CreateTestContext(string dbName) =>
        new APIDbContext(
            new DbContextOptionsBuilder<APIDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options
        );
}