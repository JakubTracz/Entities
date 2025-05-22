using Entities.Domain.People.Domain.Entities;
using Entities.Domain.People.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.Database.People;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ComplexProperty<Name>(p => p.Name, b =>
        {
            b.Property<string>(n => n.Value)
                .HasColumnName("Name");
        });
        builder.ComplexProperty<Name>(p => p.LastName, b =>
        {
            b.Property<string>(n => n.Value)
                .HasColumnName("LastName");
        });        
        builder.ComplexProperty<Age>(p => p.Age, b =>
        {
            b.Property(n => n.Value)
                .HasColumnName("Age");
        });
        builder.ComplexProperty<Address>(p => p.Address, b =>
        {
            b.Property<string>(a => a.City)
                .HasColumnName("City");
            b.Property<string>(a => a.HouseNumber)
                .HasColumnName("HouseNumber");
            b.Property<string>(a => a.PostCode)
                .HasColumnName("PostCode");
            b.Property<string>(a => a.Street)
                .HasColumnName("Street");
        });
        builder.Property<PersonId>(p => p.Id)
            .HasConversion(p => p.Id, guid => new PersonId(guid));
        builder.HasKey(p => p.Id);
    }
}