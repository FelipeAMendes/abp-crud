using System;
using Volo.Abp.Domain.Entities;

namespace Abp.Crud.Entities;

public class Person : BasicAggregateRoot<int>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Avatar { get; }
    public string Email { get; set; }
    public DateTime BirthDate { get; }

    public Person(string firstName, string lastName, string avatar, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Avatar = avatar;
        Email = email;
        BirthDate = birthDate;
    }
}
