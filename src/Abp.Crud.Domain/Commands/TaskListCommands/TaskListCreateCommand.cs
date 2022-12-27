using Abp.Crud.Entities;
using FluentValidation;
using MediatR;
using System;

namespace Abp.Crud.Commands.TaskListCommands;

public class TaskListCreateCommand : BaseCommand<TaskListCreateCommandValidation>, IRequest<ICommandResult<bool>>
{
    public string Title { get; set; }
    public string AssignedTo { get; set; }
    public DateTime DueDate { get; set; }
    public TaskListStatus? Status { get; set; }

    public TaskList ToEntity()
    {
        return new TaskList(Guid.NewGuid(), Title, AssignedTo, Status.Value, DueDate);
    }
}

public class TaskListCreateCommandValidation : AbstractValidator<TaskListCreateCommand>
{
    public TaskListCreateCommandValidation()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TaskListSpecification.TitleColumnSize);
        RuleFor(x => x.AssignedTo).MaximumLength(TaskListSpecification.AssignedToColumnSize);
        RuleFor(x => x.Status).NotNull().IsInEnum();
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(DateTime.Today);
    }
}