using Entities.Domain.Meetings.Domain.Entities;
using Entities.Domain.People.Domain.Entities;
using Entities.SharedKernel.Events.Base;

namespace Entities.Domain.Meetings.Domain.Events;

public record ParticipantsAddedToMeeting(Guid Id, Meeting Meeting, ICollection<Person> People) : BaseDomainEvent(Id);