using Entities.Domain.Meetings.Repositories;
using Entities.Domain.People.Repositories;
using Entities.Infrastructure.Database;
using Entities.Infrastructure.Database.Meetings;
using Entities.Infrastructure.Database.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static async Task<IServiceCollection> AddInfrastructure(this IServiceCollection collection)
    {
        collection
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<IMeetingRepository, MeetingRepository>()
            .AddScoped<MeetingFilteringStrategyResolver>()
            .AddScoped<PersonFilteringStrategyResolver>()
            .AddDbContext<AppDbContext>(o => { o.UseSqlite("Data Source = app.db"); });
        
        var serviceProvider = collection.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        
        return collection;
    }
}