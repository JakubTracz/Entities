using Entities.Domain.Meetings.Domain.Entities;
using Entities.Domain.Meetings.Domain.ValueObjects;
using Entities.Domain.Meetings.Repositories;
using Entities.Domain.People.Domain.ValueObjects;

namespace Entities.Application.Meetings;

public class AddMeetingHandler
{
    private readonly IMeetingRepository _meetingRepository;

    public AddMeetingHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<MeetingCreated> Handle(AddMeeting command)
    {
        var meeting = Meeting.Create(new Name(command.Title), Place.Create(command.Place), command.StartTime, new Duration(command.Duration));
        await _meetingRepository.Create(meeting);
        return new MeetingCreated(meeting.Id);
    }
}