using FluentValidation;
using MediatR;
using System;

namespace Abp.Crud.Commands.TaskListCommands;

public class TaskListDeleteCommand : BaseCommand<TaskListDeleteCommandValidation>, IRequest<ICommandResult<bool>>
{
    public TaskListDeleteCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class TaskListDeleteCommandValidation : AbstractValidator<TaskListDeleteCommand>
{
    public TaskListDeleteCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}