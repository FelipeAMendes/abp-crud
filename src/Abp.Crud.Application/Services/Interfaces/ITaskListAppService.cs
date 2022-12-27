using Abp.Crud.Commands;
using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Models;
using Abp.Crud.Pagination;
using System;
using System.Threading.Tasks;

namespace Abp.Crud.Services.Interfaces;

public interface ITaskListAppService
{
    Task<TaskListModel> GetByIdAsync(Guid id);
    Task<IPaginateModel<TaskListModel>> GetPaginatedAsync(FilterTaskListCommand filter);
    Task<ICommandResult<bool>> CreateAsync(TaskListModel taskListDto);
    Task<ICommandResult<bool>> UpdateAsync(TaskListModel taskListDto);
    Task<ICommandResult<bool>> DeleteAsync(Guid id);
}