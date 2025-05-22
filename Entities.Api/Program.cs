using Entities.Api.Responses;
using Entities.Application.Events.Handlers;
using Entities.Application.Meetings;
using Entities.Application.People;
using Entities.Domain.Meetings.Domain.Events;
using Entities.Domain.People.Domain.Events;
using Entities.Domain.People.Repositories;
using Entities.Domain.People.Specifications;
using Entities.Infrastructure;
using Entities.SharedKernel.Events;
using Entities.SharedKernel.Events.Events;
using Entities.SharedKernel.Events.Handlers;
using Entities.SharedKernel.Filtering;
using Microsoft.AspNetCore.Mvc;
using Oakton;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;
using Wolverine.RabbitMQ;

var builder = WebApplication
    .CreateBuilder(args);

builder.UseWolverine(o =>
{
    o.UseFluentValidation();
    o.PublishMessage<PersonAdded>()
        .ToRabbitExchange("added-people", c =>
        {
            c.BindQueue("added-people-queue", "added.people");
        });
    o.PublishMessage<ParticipantsAddedToMeeting>()
        .ToRabbitExchange("added-participants", c =>
        {
            c.BindQueue("added-participants-queue", "added.participants");
        });
    // o.PublishMessage<ParticipantsAddedToMeeting>()
    //     .ToRabbitQueue("participants-queue", c =>
    //     {
    //         c.BindExchange("participants-added-exchange", "participants.added");
    //     });
    o.UseRabbitMq(c =>
    {
        c.HostName = "localhost";
    })
    .AutoProvision();
    o.Discovery.IncludeAssembly(typeof(AddPersonHandler).Assembly);
});

var services = builder.Services;
services.AddHostedService<PersonEventsPublisher>();
services.AddSingleton<IDomainEventQueue, DomainEventQueue>();
services.AddHostedService<DomainEventDispatcher>();
services.AddScoped<IDomainEventHandler<PersonCreated>, PersonCreatedHandler>();
services.AddScoped<DataService>();
services.AddOpenApi("My entities API");
services.AddSwaggerGen();
await services.AddInfrastructure();
services.AddWolverineHttp();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapGet("/eu", async (IPersonRepository repository) =>
{
    var all = await repository.GetAll(new CanDrinkInEuropeSpecification());
    return all;
});
app.MapGet("/people", async (IPersonRepository repository, string? filterBy) =>
{
    var all = await repository.GetAll(filterBy);
    return all;
});
app.MapGet("/meetings", async (DataService dataService, string? filterBy) =>
{
    var all = await dataService.GetAll(filterBy);
    return Results.Ok(all);
})
.WithTags("Meetings APIs");
app.MapGet("/us", async (IPersonRepository repository) =>
{
    var all = await repository.GetAll(new CanDrinkInUsSpecification());
    return all;
});

app.MapWolverineEndpoints(o =>
{
    o.UseFluentValidationProblemDetailMiddleware();
});
app.UseHttpsRedirection();
await app.RunOaktonCommands(args);


public static class PersonCreationEndpoint
{
    [Tags("People APIs")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [WolverinePost("/person")]
    public static async Task<PersonAddedResponse> Post(AddPerson command, IMessageBus messageBus)
    {
        var result = await messageBus.InvokeAsync<PersonAdded>(command);
        return new PersonAddedResponse(result);
    }
}

public static class MeetingCreationEndpoint
{
    [Tags("Meetings APIs")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [WolverinePost("/meeting")]
    public static async Task<MeetingCreatedResponse> Post(AddMeeting command, IMessageBus messageBus)
    {
        var result = await messageBus.InvokeAsync<MeetingCreated>(command);
        return new MeetingCreatedResponse(result);
    }    
    
    [Tags("Meetings APIs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [WolverinePost("/meetings/invite")]
    public static async Task<IResult> Put(InviteParticipants command, IMessageBus messageBus)
    {
        await messageBus.InvokeAsync(command);
        return Results.Ok();
    }
}