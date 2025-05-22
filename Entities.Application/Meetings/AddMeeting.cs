namespace Entities.Application.Meetings;

public record AddMeeting(string Title, string Place, DateTimeOffset StartTime, decimal Duration);