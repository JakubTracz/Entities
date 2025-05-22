using Entities.Application.Meetings;
using Wolverine.Http;

namespace Entities.Api.Responses;

public record MeetingCreatedResponse(MeetingCreated MeetingCreated)
    : CreationResponse($"/meetings/{MeetingCreated.Id}");