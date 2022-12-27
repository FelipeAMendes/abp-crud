using Abp.Crud.Entities;
using Bogus;
using System;

namespace Abp.Crud.Commands.TaskListCommands;

[CollectionDefinition(nameof(TaskListCommandTestsCollection))]
public class TaskListCommandTestsCollection : ICollectionFixture<TaskListCommandTestsFixture> { }

public class TaskListCommandTestsFixture
{
    public TaskListCreateCommand GetValidCreateCommand()
    {
        return new Faker<TaskListCreateCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Lorem.Sentence(3);
                var assignedTo = x.Person.FullName;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today, DateTime.Today.AddDays(15));

                return new TaskListCreateCommand
                {
                    Title = title,
                    AssignedTo = assignedTo,
                    Status = status,
                    DueDate = dueDate
                };
            });
    }

    public TaskListCreateCommand GetInvalidCreateCommand()
    {
        return new Faker<TaskListCreateCommand>()
            .CustomInstantiator(x =>
            {
                var title = string.Empty;
                var assignedTo = string.Empty;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-1));

                return new TaskListCreateCommand
                {
                    Title = title,
                    AssignedTo = assignedTo,
                    Status = status,
                    DueDate = dueDate
                };
            });
    }

    public TaskListUpdateCommand GetValidUpdateCommand()
    {
        return new Faker<TaskListUpdateCommand>()
            .CustomInstantiator(x =>
            {
                var title = x.Lorem.Sentence(3);
                var assignedTo = x.Person.FullName;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today, DateTime.Today.AddDays(15));

                return new TaskListUpdateCommand
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    AssignedTo = assignedTo,
                    Status = status,
                    DueDate = dueDate
                };
            });
    }

    public TaskListUpdateCommand GetInvalidUpdateCommand()
    {
        return new Faker<TaskListUpdateCommand>()
            .CustomInstantiator(x =>
            {
                var title = string.Empty;
                var assignedTo = string.Empty;
                var status = x.Random.Enum<TaskListStatus>();
                var dueDate = x.Date.Between(DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-1));

                return new TaskListUpdateCommand
                {
                    Id = Guid.Empty,
                    Title = title,
                    AssignedTo = assignedTo,
                    Status = status,
                    DueDate = dueDate
                };
            });
    }

    public TaskListDeleteCommand GetValidDeleteCommand()
    {
        return new Faker<TaskListDeleteCommand>()
            .CustomInstantiator(_ => new TaskListDeleteCommand(Guid.NewGuid()));
    }

    public TaskListDeleteCommand GetInvalidDeleteCommand()
    {
        return new Faker<TaskListDeleteCommand>()
            .CustomInstantiator(_ => new TaskListDeleteCommand(Guid.Empty));
    }
}