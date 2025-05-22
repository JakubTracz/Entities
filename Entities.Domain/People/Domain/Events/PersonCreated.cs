using Entities.Domain.People.Domain.ValueObjects;
using Entities.SharedKernel.Events.Base;

namespace Entities.Domain.People.Domain.Events;

public record PersonCreated(Guid Id, PersonId PersonId) : BaseDomainEvent(Id);