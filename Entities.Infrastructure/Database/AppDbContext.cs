using Microsoft.EntityFrameworkCore;
using Meeting = Entities.Domain.Meetings.Domain.Entities.Meeting;
using Person = Entities.Domain.People.Domain.Entities.Person;

namespace Entities.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Meeting> Meetings { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}