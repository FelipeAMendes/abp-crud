using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Entities;
using Abp.Crud.Pagination;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.Crud.Repositories.Interfaces;

public interface ITaskListRepository: IRepository<TaskList, Guid>
{
    Task<TaskList> GetByIdAsync(Guid id);
    Task<ICollection<TaskList>> GetAllAsync(string title);
    Task<IPaginateModel<TaskList>> GetPaginatedAsync(FilterTaskListCommand filter);
    Task<bool> CreateAsync(TaskList taskList, CancellationToken token);
    Task<bool> UpdateAsync(TaskList taskList, CancellationToken token);
    Task<bool> DeleteAsync(TaskList taskList);
}