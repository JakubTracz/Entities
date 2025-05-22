using Entities.Domain.Meetings.Domain.Events;
using Entities.Domain.Meetings.Domain.ValueObjects;
using Entities.Domain.People.Domain.Entities;
using Entities.Domain.People.Domain.ValueObjects;
using Entities.SharedKernel.Attributes;
using Entities.SharedKernel.SharedKernel;

namespace Entities.Domain.Meetings.Domain.Entities;

[FilterableEntity]
public partial class Meeting : BaseEntity<Guid>
{
    public Name Name { get; }
    public Place Place { get; }
    public DateTimeOffset StartTime { get;  }
    public Duration Duration { get; }
    public List<Person> Participants { get; } = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Meeting()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        
    }
    
    private Meeting(Name name, Place place, DateTimeOffset startTime, Duration duration)
    {
        Name = name;
        Place = place;
        StartTime = startTime;
        Duration = duration;
    }
    
    public static Meeting Create(Name name, Place place, DateTimeOffset startTime, Duration duration)
    {
        return new Meeting(name, place, startTime, duration);
    }
    
    public void AddParticipants(ICollection<Person> people)
    {
        foreach (var person in people)
        {
            if (Participants.All(p => p.Id != person.Id))
            {
                Participants.Add(person);
            }
        }
        
        AddDomainEvent(new ParticipantsAddedToMeeting(Id, this, people));
    }
}