using Entities.Domain.Meetings.Repositories;
using Microsoft.EntityFrameworkCore;
using Meeting = Entities.Domain.Meetings.Domain.Entities.Meeting;

namespace Entities.Infrastructure.Database.Meetings;

public class MeetingRepository : IMeetingRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly MeetingFilteringStrategyResolver _filteringStrategyResolver;

    public MeetingRepository(MeetingFilteringStrategyResolver filteringStrategyResolver, AppDbContext appDbContext)
    {
        _filteringStrategyResolver = filteringStrategyResolver;
        _appDbContext = appDbContext;
    }
    
    public Task<List<Meeting>> GetAll(string? filterBy)
    {
        var filteringStrategy = _filteringStrategyResolver.ResolveEfCoreFilteringStrategy(filterBy);

        var query = _appDbContext.Meetings
            .AsQueryable();

        foreach (var expression in filteringStrategy.Expressions)
        {
            query = query.Where(expression);
        }

        return query
            .ToListAsync();
    }

    public async Task<Meeting> Create(Meeting meeting)
    {
        _appDbContext.Meetings.Add(meeting);
        await _appDbContext.SaveChangesAsync();
        return meeting;
    }

    public Task<Meeting> Update(Meeting meeting)
    {
        _appDbContext.Meetings.Update(meeting);
        return Task.FromResult(meeting);
    }

    public Task<Meeting?> GetById(Guid id)
    {
        var meeting = _appDbContext.Meetings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        return meeting;
    }
}