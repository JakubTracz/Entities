using Entities.Domain.Meetings.Domain.Entities;
using Entities.Domain.Meetings.Repositories;

namespace Entities.Application.Meetings;

public class DataService
{
    private readonly IMeetingRepository _repository;

    public DataService(IMeetingRepository repository)
    {
        _repository = repository;
    }
    
    public Task<List<Meeting>> GetAll(string? filterBy)
    {
        return _repository.GetAll(filterBy);
    }
}