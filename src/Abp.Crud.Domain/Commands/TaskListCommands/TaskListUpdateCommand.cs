using Abp.Crud.Entities;
using FluentValidation;
using MediatR;
using System;

namespace Abp.Crud.Commands.TaskListCommands;

public class TaskListUpdateCommand : BaseCommand<TaskListUpdateCommandValidation>, IRequest<ICommandResult<bool>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string AssignedTo { get; set; }
    public DateTime DueDate { get; set; }
    public TaskListStatus? Status { get; set; }

    public TaskList FromEntity(TaskList taskList)
    {
        taskList.Update(Title, AssignedTo, Status.Value, DueDate);
        return taskList;
    }
}

public class TaskListUpdateCommandValidation : AbstractValidator<TaskListUpdateCommand>
{
    public TaskListUpdateCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TaskListSpecification.TitleColumnSize);
        RuleFor(x => x.AssignedTo).MaximumLength(TaskListSpecification.AssignedToColumnSize);
        RuleFor(x => x.Status).NotNull().IsInEnum();
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(DateTime.Today);
    }
}