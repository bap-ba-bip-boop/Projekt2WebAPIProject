using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SharedResources.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Project>? Projects { get; set; }
    public DbSet<TidsRegistrering>? TidsRegistrerings { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
