using Entities.Domain.Meetings.Domain.Entities;
using Entities.Domain.Meetings.Domain.ValueObjects;
using Entities.Domain.People.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.Database.Meetings;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ComplexProperty<Name>(p => p.Name, b =>
        {
            b.Property(n => n.Value)
                .HasColumnName("Name");
        });
        builder.ComplexProperty<Place>(p => p.Place, b =>
        {
            b.Property(n => n.Value)
                .HasColumnName("Place");
        });        
        builder.ComplexProperty<Duration>(p => p.Duration, b =>
        {
            b.Property(n => n.Value)
                .HasColumnName("Duration");
        });
        builder.Property(m => m.StartTime)
            .HasColumnName("StartTime");
        builder.Navigation(m => m.Participants);
        builder.HasKey(p => p.Id);
    }
}