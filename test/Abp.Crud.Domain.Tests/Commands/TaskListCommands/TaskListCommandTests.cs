namespace Abp.Crud.Commands.TaskListCommands;

[Collection(nameof(TaskListCommandTestsCollection))]
public class TaskListCommandTests
{
    private readonly TaskListCommandTestsFixture _fixture;

    public TaskListCommandTests(TaskListCommandTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid create command")]
    [Trait("Category", "Task List Command")]
    public void TaskListCreateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var createCommand = _fixture.GetValidCreateCommand();

        // Act
        var validationResult = createCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid create command")]
    [Trait("Category", "Task List Command")]
    public void TaskListCreateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var createCommand = _fixture.GetInvalidCreateCommand();

        // Act
        var validationResult = createCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(2, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid update command")]
    [Trait("Category", "Task List Command")]
    public void TaskListUpdateCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var updateCommand = _fixture.GetValidUpdateCommand();

        // Act
        var validationResult = updateCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid update command")]
    [Trait("Category", "Task List Command")]
    public void TaskListUpdateCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var updateCommand = _fixture.GetInvalidUpdateCommand();

        // Act
        var validationResult = updateCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Equal(3, validationResult.Errors.Count);
    }

    [Fact(DisplayName = "Valid delete command")]
    [Trait("Category", "Task List Command")]
    public void TaskListDeleteCommandIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        // Arrange
        var deleteCommand = _fixture.GetValidDeleteCommand();

        // Act
        var validationResult = deleteCommand.Validate();

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.Empty(validationResult.Errors);
    }

    [Fact(DisplayName = "Invalid delete command")]
    [Trait("Category", "Task List Command")]
    public void TaskListDeleteCommandIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        // Arrange
        var deleteCommand = _fixture.GetInvalidDeleteCommand();

        // Act
        var validationResult = deleteCommand.Validate();

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.Single(validationResult.Errors);
    }
}