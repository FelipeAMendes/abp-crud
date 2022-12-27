using Abp.Crud.Commands;
using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Entities;
using Abp.Crud.Models;
using Abp.Crud.Pagination;
using Abp.Crud.Repositories.Interfaces;
using Abp.Crud.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.ObjectMapping;

namespace Abp.Crud.Services;

public class TaskListAppService : BaseAppService, ITaskListAppService
{
    private readonly IObjectMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ITaskListRepository _taskListRepository;
    private readonly IDistributedCache<TaskListModel> _cache;

    public TaskListAppService(IObjectMapper mapper, IMediator mediator, ITaskListRepository taskListRepository, IDistributedCache<TaskListModel> cache)
    {
        _mapper = mapper;
        _mediator = mediator;
        _taskListRepository = taskListRepository;
        _cache = cache;
    }

    public async Task<IPaginateModel<TaskListModel>> GetPaginatedAsync(FilterTaskListCommand filter)
    {
        var paginatedResult = await _taskListRepository.GetPaginatedAsync(filter);
        var taskListDto = _mapper.Map<ICollection<TaskList>, ICollection<TaskListModel>>(paginatedResult.Items.ToList());

        return new PaginateModel<TaskListModel>(
            paginatedResult.CurrentPage,
            paginatedResult.Pages,
            paginatedResult.PageSize,
            paginatedResult.Total,
            filter.OrderBy,
            taskListDto);
    }

    public async Task<TaskListModel> GetByIdAsync(Guid id)
    {
        return await _cache.GetOrAddAsync(
            id.ToString(),
            async () =>
            {
                var taskList = await _taskListRepository.GetAsync(x => x.Id == id);
                var taskListDto = _mapper.Map<TaskList, TaskListModel>(taskList);
                return taskListDto;
            },
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            }
        );
    }

    public async Task<ICommandResult<bool>> CreateAsync(TaskListModel taskListDto)
    {
        var createCommand = new TaskListCreateCommand
        {
            Title = taskListDto.Title,
            AssignedTo = taskListDto.AssignedTo,
            Status = taskListDto.Status,
            DueDate = taskListDto.DueDate
        };

        var commandResult = await _mediator.Send(createCommand);

        return commandResult;
    }

    public async Task<ICommandResult<bool>> UpdateAsync(TaskListModel taskListDto)
    {
        var updateCommand = new TaskListUpdateCommand
        {
            Id = taskListDto.Id,
            Title = taskListDto.Title,
            AssignedTo = taskListDto.AssignedTo,
            Status = taskListDto.Status,
            DueDate = taskListDto.DueDate
        };

        var commandResult = await _mediator.Send(updateCommand);

        return commandResult;
    }

    public async Task<ICommandResult<bool>> DeleteAsync(Guid id)
    {
        var deleteCommand = new TaskListDeleteCommand(id);

        var commandResult = await _mediator.Send(deleteCommand);

        return commandResult;
    }
}