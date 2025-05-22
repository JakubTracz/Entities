using Entities.Domain.Meetings.Repositories;
using Entities.Domain.People.Repositories;
using Entities.Domain.People.Specifications;
using Wolverine;

namespace Entities.Application.Meetings;

public class InviteParticipantsHandler
{
    private readonly IMessageBus _bus;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IPersonRepository _personRepository;

    public InviteParticipantsHandler(IMeetingRepository meetingRepository, IPersonRepository personRepository,
        IMessageBus bus)
    {
        _meetingRepository = meetingRepository;
        _personRepository = personRepository;
        _bus = bus;
    }

    public async Task Handle(InviteParticipants command)
    {
        var meeting = await _meetingRepository.GetById(command.Id);

        if (meeting is null)
        {
            throw new Exception("Meeting not found");
        }

        var specification = new AllByIdSpecification(command.ParticipantIds);
        var participants = await _personRepository.GetAll(specification);

        if (participants.Count == 0)
        {
            throw new Exception("No participants found");
        }

        meeting.AddParticipants(participants);
        await _meetingRepository.Update(meeting);

        foreach (var domainEvent in meeting.DomainEvents)
        {
            await _bus.PublishAsync(domainEvent);
        }
        
        meeting.ClearDomainEvents();
    }
}