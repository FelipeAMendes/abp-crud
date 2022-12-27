using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Entities;
using AutoFixture;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq.Expressions;
using System.Threading;
using Abp.Crud.Repositories.Interfaces;
using Volo.Abp.Domain.Repositories;

namespace Abp.Crud.Handlers.TaskListHandlers;

[CollectionDefinition(nameof(TaskListHandlerTestsCollection))]
public class TaskListHandlerTestsCollection : ICollectionFixture<TaskListHandlerTestsFixture> { }

public class TaskListHandlerTestsFixture
{
    private readonly IFixture _autoFixture;
    private readonly AutoMocker _autoMocker;

    public TaskListHandlerTestsFixture()
    {
        _autoMocker = new AutoMocker();
        _autoFixture = new Fixture();
    }

    public TaskListHandler GetHandlerInstance()
    {
        return _autoMocker.CreateInstance<TaskListHandler>();
    }

    public TaskListCreateCommand GetValidCreateCommand()
    {
        var createCommand = _autoFixture.Create<TaskListCreateCommand>();
         createCommand.DueDate = DateTime.Today.AddDays(1);
         return createCommand;
    }

    public TaskListCreateCommand GetInvalidCreateCommand()
    {
        return new Faker<TaskListCreateCommand>()
            .CustomInstantiator(_ => new TaskListCreateCommand());
    }

    public Mock<ITaskListRepository> GetCreateRepositoryMock(bool success)
    {
        var mockRepository = _autoMocker.GetMock<ITaskListRepository>();

        mockRepository
            .Setup(x => x.CreateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        return mockRepository;
    }

    public Mock<ITaskListRepository> CreateGetByIdRepositoryMock()
    {
        var taskList = new TaskList(It.IsAny<Guid>(), "title", "resp", TaskListStatus.Completed, DateTime.Today.AddDays(1));

        var mockRepository = _autoMocker.GetMock<ITaskListRepository>();

        mockRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(taskList);

        return mockRepository;
    }

    public Mock<ITaskListRepository> CreateNonExistingGetByIdRepositoryMock()
    {
        var mockRepository = _autoMocker.GetMock<ITaskListRepository>();

        mockRepository
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        return mockRepository;
    }

    public TaskListUpdateCommand GetValidUpdateCommand()
    {
        var updateCommand = _autoFixture.Create<TaskListUpdateCommand>();
        updateCommand.DueDate = DateTime.Today.AddDays(1);
        return updateCommand;
    }

    public TaskListUpdateCommand GetInvalidUpdateCommand()
    {
        return new Faker<TaskListUpdateCommand>()
            .CustomInstantiator(_ => new TaskListUpdateCommand());
    }

    public Mock<ITaskListRepository> GetUpdateRepositoryMock(bool success)
    {
        var mockRepository = _autoMocker.GetMock<ITaskListRepository>();
        
        mockRepository
            .Setup(x => x.UpdateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(success);

        return mockRepository;
    }

    public TaskListDeleteCommand GetValidDeleteCommand()
    {
        return _autoFixture.Create<TaskListDeleteCommand>();
    }
}