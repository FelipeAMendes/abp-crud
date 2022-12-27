namespace Abp.Crud.Entities.TaskListEntity;

[Collection(nameof(TaskListTestsCollection))]
public class TaskListTests
{
    private readonly TaskListTestsFixture _fixture;

    public TaskListTests(TaskListTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid Task List")]
    [Trait("Category", "Task List Entity")]
    public void TaskListIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        //Arrange
        var taskList = _fixture.GetValidTaskList();

        //Act
        var resultValidation = taskList.Validate();

        //Assert
        Assert.True(resultValidation.IsValid);
        Assert.Empty(resultValidation.Errors);
    }

    [Fact(DisplayName = "Invalid Task List")]
    [Trait("Category", "Task List Entity")]
    public void TaskListIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        //Arrange
        var taskList = _fixture.GetInvalidTaskList();

        //Act
        var resultValidation = taskList.Validate();

        //Assert
        Assert.False(resultValidation.IsValid);
        Assert.Equal(2, resultValidation.Errors.Count);
    }
}