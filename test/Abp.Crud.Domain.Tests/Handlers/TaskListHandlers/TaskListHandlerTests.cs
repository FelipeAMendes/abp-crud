using System.Linq;
using Abp.Crud.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.Crud.Handlers.TaskListHandlers;

[Collection(nameof(TaskListHandlerTestsCollection))]
public class TaskListHandlerTests
{
    private readonly TaskListHandlerTestsFixture _fixture;
    private readonly TaskListHandler _taskListHandler;

    public TaskListHandlerTests(TaskListHandlerTestsFixture fixture)
    {
        _fixture = fixture;
        _taskListHandler = _fixture.GetHandlerInstance();

        _fixture.CreateGetByIdRepositoryMock();
    }

    [Fact(DisplayName = "Task List is not valid - create")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListIsNotValid_HandleCreateIsCalled_ReturnsFalse()
    {
        //Arrange
        var createCommand = _fixture.GetInvalidCreateCommand();
        _fixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _taskListHandler.Handle(createCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(expected: 3, commandResult.Errors.Count());
    }

    [Fact(DisplayName = "Task List repository with error - create")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListRepositoryWithError_HandleCreateIsCalled_ReturnsCreationError()
    {
        //Arrange
        var createCommand = _fixture.GetValidCreateCommand();
        var repositoryMock = _fixture.GetCreateRepositoryMock(false);

        //Act
        var commandResult = await _taskListHandler.Handle(createCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.CreationError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Create Task List")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListIsValid_HandleCreateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var createCommand = _fixture.GetValidCreateCommand();
        var repositoryMock = _fixture.GetCreateRepositoryMock(true);

        //Act
        var commandResult = await _taskListHandler.Handle(createCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.CreateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Task List is not valid - update")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListIsNotValid_HandleUpdateIsCalled_ReturnsFalse()
    {
        //Arrange
        var updateCommand = _fixture.GetInvalidUpdateCommand();

        //Act
        var commandResult = await _taskListHandler.Handle(updateCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(expected:4, commandResult.Errors.Count());
    }

    [Fact(DisplayName = "Task List does not exist - update")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListDoesNotExist_HandleUpdateIsCalled_ReturnsNotFound()
    {
        //Arrange
        var updateCommand = _fixture.GetValidUpdateCommand();
        _fixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _taskListHandler.Handle(updateCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Update Task List")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListIsValid_HandleUpdateIsCalled_ReturnsSuccess()
    {
        //Arrange
        var updateCommand = _fixture.GetValidUpdateCommand();
        var repositoryMock = _fixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _taskListHandler.Handle(updateCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Task List does not exist - delete")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListDoesNotExist_HandleDeleteIsCalled_ReturnsNotFound()
    {
        //Arrange
        var deleteCommand = _fixture.GetValidDeleteCommand();
        _fixture.CreateNonExistingGetByIdRepositoryMock();

        //Act
        var commandResult = await _taskListHandler.Handle(deleteCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.ItemNotFound, commandResult.ValidationType);
    }

    [Fact(DisplayName = "Task List repository with error - delete")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListRepositoryWithError_HandleDeleteIsCalled_ReturnsRemovalError()
    {
        //Arrange
        var deleteCommand = _fixture.GetValidDeleteCommand();
        var repositoryMock = _fixture.GetUpdateRepositoryMock(false);

        //Act
        var commandResult = await _taskListHandler.Handle(deleteCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.False(commandResult.Success);
        Assert.Equal(ValidationType.RemovalError, commandResult.ValidationType);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }

    [Fact(DisplayName = "Task List Delete")]
    [Trait("Category", "Task List Handler")]
    public async Task TaskListIsValid_HandleDeleteIsCalled_ReturnsSuccess()
    {
        //Arrange
        var deleteCommand = _fixture.GetValidDeleteCommand();
        var repositoryMock = _fixture.GetUpdateRepositoryMock(true);

        //Act
        var commandResult = await _taskListHandler.Handle(deleteCommand, It.IsAny<CancellationToken>());

        //Assert
        Assert.True(commandResult.Success);
        Assert.Empty(commandResult.Errors);

        repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TaskList>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}