using Meeting = Entities.Domain.Meetings.Domain.Entities.Meeting;

namespace Entities.Domain.Meetings.Repositories;

public interface IMeetingRepository
{
    Task<List<Meeting>> GetAll(string? filterBy);
    Task<Meeting> Create(Meeting meeting);
    Task<Meeting> Update(Meeting meeting);
    Task<Meeting?> GetById(Guid id);
}