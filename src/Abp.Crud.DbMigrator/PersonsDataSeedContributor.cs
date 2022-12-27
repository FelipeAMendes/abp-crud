using Bogus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using DbContext = Abp.Crud.EntityFrameworkCore.DbContext;
using Person = Abp.Crud.Entities.Person;

namespace Abp.Crud.DbMigrator;

public class PersonsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public ILogger<PersonsDataSeedContributor> Logger { get; set; }
    private readonly IRepository<Person, int> _personRepository;
    private readonly DbContext _dbContext;

    public PersonsDataSeedContributor(IRepository<Person, int> personRepository, DbContext dbContext)
    {
        _personRepository = personRepository;
        _dbContext = dbContext;
        Logger = NullLogger<PersonsDataSeedContributor>.Instance;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _personRepository.GetCountAsync() > 0)
            return;

        const int countPersons = 100000;
        var list = GetPersons(countPersons);

        using var scope = new TransactionScope();
        try
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;

            var count = 0;
            foreach (var entityToInsert in list)
            {
                Logger.LogInformation($"First Name: {entityToInsert.FirstName}; count: {count}");
                ++count;
                AddToContext(entityToInsert, count, 500);
            }

            await _dbContext.SaveChangesAsync();
        }
        finally
        {
            _dbContext?.DisposeAsync();
        }

        scope.Complete();
    }

    private static ConcurrentBag<Person> GetPersons(int count)
    {
        var person = new Faker<Person>()
            .CustomInstantiator(x =>
            {
                var firstName = x.Person.FirstName;
                var lastName = x.Person.LastName;
                var avatar = x.Person.Avatar;
                var email = x.Internet.Email(firstName, lastName);
                var birthDate = x.Date.Between(DateTime.Today.AddYears(-80), DateTime.Today.AddYears(-20));

                return new Person(firstName, lastName, avatar, email, birthDate);
            });

        return new ConcurrentBag<Person>(person.Generate(count));
    }

    private void AddToContext(Person person, int count, int commitCount)
    {
        _dbContext.Set<Person>().Add(person);

        if (count % commitCount == 0)
        {
            _dbContext.SaveChanges();
        }
    }
}