using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model;

public class APIDbContext : DbContext
{
    public DbSet<Customer>? Customers { get; set; }
    public APIDbContext( DbContextOptions<APIDbContext> options) : base(options)
    {

    }
}
