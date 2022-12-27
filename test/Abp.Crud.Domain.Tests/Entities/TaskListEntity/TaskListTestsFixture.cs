using Bogus;
using System;

namespace Abp.Crud.Entities.TaskListEntity;

[CollectionDefinition(nameof(TaskListTestsCollection))]
public class TaskListTestsCollection : ICollectionFixture<TaskListTestsFixture> { }

public class TaskListTestsFixture
{
    public TaskList GetValidTaskList()
    {
        return new Faker<TaskList>()
            .CustomInstantiator(x =>
            {
                var title = x.Lorem.Sentence(3);
                var assignedTo = x.Person.FullName;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today, DateTime.Today.AddDays(15));

                return new TaskList(Guid.NewGuid(), title, assignedTo, status, dueDate);
            });
    }

    public TaskList GetInvalidTaskList()
    {
        return new Faker<TaskList>()
            .CustomInstantiator(x =>
            {
                var title = string.Empty;
                var assignedTo = string.Empty;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-1));

                return new TaskList(Guid.NewGuid(), title, assignedTo, status, dueDate);
            });
    }
}