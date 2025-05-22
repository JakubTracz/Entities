using Entities.SharedKernel.Events.Events;
using Wolverine.Http;

namespace Entities.Api.Responses;

public record PersonAddedResponse(PersonAdded PersonAdded)
    : CreationResponse($"/people/{PersonAdded.Id}");