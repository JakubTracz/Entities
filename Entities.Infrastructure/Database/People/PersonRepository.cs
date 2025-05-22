using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Entities.Domain.People.Domain.ValueObjects;
using Entities.Domain.People.Repositories;
using Entities.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;
using Person = Entities.Domain.People.Domain.Entities.Person;

namespace Entities.Infrastructure.Database.People;

internal class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly PersonFilteringStrategyResolver _personFilteringStrategyResolver;
    private readonly IDomainEventQueue _domainEventQueue;

    public PersonRepository(AppDbContext appDbContext,
        PersonFilteringStrategyResolver personFilteringStrategyResolver, IDomainEventQueue domainEventQueue)
    {
        _appDbContext = appDbContext;
        _personFilteringStrategyResolver = personFilteringStrategyResolver;
        _domainEventQueue = domainEventQueue;
    }

    public Task<List<Person>> GetAll(ISpecification<Person> specification)
    {
        return _appDbContext.People
            .WithSpecification(specification)
            .ToListAsync();
    }

    public Task<List<Person>> GetAll(string? filterBy)
    {
        var filteringStrategy = _personFilteringStrategyResolver.ResolveEfCoreFilteringStrategy(filterBy);
        var query = _appDbContext.People
            .AsQueryable();

        foreach (var expression in filteringStrategy.Expressions)
        {
            query = query.Where(expression);
        }

        return query
            .ToListAsync();
    }
    
    public async Task<Person> Create(Person person)
    {
        _appDbContext.Add(person);
        await _appDbContext.SaveChangesAsync();
        await DomainEventDispatcherHelper.DispatchDomainEventsAsync(_appDbContext, _domainEventQueue);
        return person;
    }

    public Task<Person?> GetById(PersonId id)
    {
        return _appDbContext.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}