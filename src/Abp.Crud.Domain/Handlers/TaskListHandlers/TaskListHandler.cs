using Abp.Crud.Commands;
using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Abp.Crud.Handlers.TaskListHandlers;

public class TaskListHandler : BaseHandler,
    IRequestHandler<TaskListCreateCommand, ICommandResult<bool>>,
    IRequestHandler<TaskListUpdateCommand, ICommandResult<bool>>,
    IRequestHandler<TaskListDeleteCommand, ICommandResult<bool>>
{
    private readonly ITaskListRepository _repository;

    public TaskListHandler(ITaskListRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult<bool>> Handle(TaskListCreateCommand command, CancellationToken cancellationToken)
    {
        var resultValidation = command.Validate();
        if (!resultValidation.IsValid)
            return CreateErrorCommandResult(resultValidation.Errors);

        var entity = command.ToEntity();
        var result = await _repository.CreateAsync(entity, cancellationToken);
        
        return CreateDefaultCommandResult(result, ValidationType.CreationError);
    }

    public async Task<ICommandResult<bool>> Handle(TaskListUpdateCommand command, CancellationToken cancellationToken)
    {
        var resultValidation = command.Validate();
        if (!resultValidation.IsValid)
            return CreateErrorCommandResult(resultValidation.Errors);

        var existingTaskList = await _repository.GetByIdAsync(command.Id);
        if (existingTaskList is null)
            return CreateNotFoundCommandResult();

        var entity = command.FromEntity(existingTaskList);
        var result = await _repository.UpdateAsync(entity, token: cancellationToken);

        return CreateDefaultCommandResult(result, ValidationType.ChangeError);
    }

    public async Task<ICommandResult<bool>> Handle(TaskListDeleteCommand command, CancellationToken cancellationToken)
    {
        var resultValidation = command.Validate();
        if (!resultValidation.IsValid)
            return CreateErrorCommandResult(resultValidation.Errors);

        var existingTaskList = await _repository.GetByIdAsync(command.Id);
        if (existingTaskList is null)
            return CreateNotFoundCommandResult();

        existingTaskList.Delete();
        var result = await _repository.UpdateAsync(existingTaskList, token: cancellationToken);

        return CreateDefaultCommandResult(result, ValidationType.RemovalError);
    }
}