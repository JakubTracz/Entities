using Entities.SharedKernel.Events.Events;

namespace Entities.Consumer.Handlers;

public class AddPersonHandler
{
    private readonly ILogger<AddPersonHandler> _logger;

    public AddPersonHandler(ILogger<AddPersonHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(PersonAdded command)
    {
        _logger.LogInformation("Person consumed: {Name}", command.Name);
    }
}