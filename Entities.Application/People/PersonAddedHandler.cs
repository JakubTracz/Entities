using Entities.SharedKernel.Events.Events;
using Microsoft.Extensions.Logging;

namespace Entities.Application.People;

// public class PersonAddedHandler
// {
//     private readonly ILogger<PersonAddedHandler> _logger;
//
//     public PersonAddedHandler(ILogger<PersonAddedHandler> logger)
//     {
//         _logger = logger;
//     }
//     
//     public async Task Handle(PersonAdded personAdded, CancellationToken cancellationToken)
//     {
//         await Task.Delay(1000, cancellationToken);
//         _logger.LogInformation("Person added: {Name}", personAdded.Name);
//     }
// }