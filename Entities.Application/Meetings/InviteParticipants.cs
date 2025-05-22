namespace Entities.Application.Meetings;

public record InviteParticipants(Guid Id, List<Guid> ParticipantIds);