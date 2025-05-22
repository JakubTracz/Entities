using System.Text.Json;
using Entities.Application.Meetings;
using Entities.Application.People;
using Entities.SharedKernel.Events.Events;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", async () =>
{
    var client = new HttpClient();
    var addPerson = new AddPerson(
        "",
        "Doe",
        "New York",
        "10001",
        "5th Avenue",
        "10A",
        30);
    var response = await client.PostAsJsonAsync("https://localhost:7213/person", addPerson);
    if (response.IsSuccessStatusCode)
    {
        var con = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(con);
        var personAddedElement = json.RootElement.GetProperty("personAdded");
        var personAdded = JsonSerializer.Deserialize<PersonAdded>(personAddedElement.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return Results.Ok(personAdded);
    }

    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

    //return problem details
    return Results.BadRequest(problemDetails);
});
app.MapGet("/m", async () =>
{
    var client = new HttpClient();
    var addMeeting = new AddMeeting(
        "1:1",
        "Teams",
        DateTimeOffset.Now, 
        60);
    var response = await client.PostAsJsonAsync("https://localhost:7213/meeting", addMeeting);
    if (response.IsSuccessStatusCode)
    {
        var con = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(con);
        var personAddedElement = json.RootElement.GetProperty("meetingCreated");
        var personAdded = JsonSerializer.Deserialize<PersonAdded>(personAddedElement.GetRawText(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return Results.Ok(personAdded);
    }

    var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

    //return problem details
    return Results.BadRequest(problemDetails);
});
app.MapGet("/i", async () =>
{
    var client = new HttpClient();
    var addMeeting = new InviteParticipants(Guid.Parse("a224a3ab-da48-4c98-818b-5d389302615a"), 
        [
            Guid.Parse("0196f504-f28a-75ac-8234-9caad09fc830"),
            Guid.Parse("0196f528-2400-7375-8772-c13ce6d054f7")
        ]);
    var response = await client.PostAsJsonAsync("https://localhost:7213/meetings/invite", addMeeting);
    if (response.IsSuccessStatusCode)
    {
        var con = await response.Content.ReadAsStringAsync();
        return Results.Ok(con);
    }

    var con2 = await response.Content.ReadAsStringAsync();
    //return problem details
    return Results.BadRequest(con2);
});

app.Run();