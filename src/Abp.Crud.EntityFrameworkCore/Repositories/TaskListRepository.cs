using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Entities;
using Abp.Crud.Pagination;
using Abp.Crud.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.Crud.Repositories;

public class TaskListRepository : EfCoreRepository<EntityFrameworkCore.DbContext, TaskList, Guid>, ITaskListRepository
{
    public TaskListRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider)
        : base(dbContextProvider) { }

    public async Task<TaskList> GetByIdAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext
            .Set<TaskList>()
            .FindAsync(id);
    }

    public async Task<ICollection<TaskList>> GetAllAsync(string title)
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext
            .Set<TaskList>()
            .Where(x => x.Title.Contains(title))
            .ToListAsync();
    }

    public async Task<IPaginateModel<TaskList>> GetPaginatedAsync(FilterTaskListCommand filter)
    {
        var dbContext = await GetDbContextAsync();

        var queryable = dbContext
            .Set<TaskList>()
            .WhereIf(!string.IsNullOrEmpty(filter.Title), x => x.Title.ToLower().Contains(filter.Title.ToLower()));

        var countToSkip = (filter.CurrentPage - 1) * filter.PageSize;

        var query = queryable
            .OrderBy(x => x.CreationTime)
            .Skip(countToSkip ?? 0)
            .Take(filter.PageSize.GetValueOrDefault());

        var countRows = await queryable.CountAsync();
        var countPages = countRows / filter.PageSize.GetValueOrDefault();

        var queryResult = await query.ToListAsync();

        return new PaginateModel<TaskList>(filter.CurrentPage, countPages, filter.PageSize.GetValueOrDefault(), countRows, filter.OrderBy, queryResult);
    }

    public async Task<bool> CreateAsync(TaskList taskList, CancellationToken token)
    {
        var dbContext = await GetDbContextAsync();

        await dbContext
            .Set<TaskList>()
            .AddAsync(taskList, token);

        return true;
    }

    public async Task<bool> UpdateAsync(TaskList taskList, CancellationToken token)
    {
        var dbContext = await GetDbContextAsync();

        dbContext
            .Set<TaskList>()
            .Update(taskList);

        return true;
    }

    public async Task<bool> DeleteAsync(TaskList taskList)
    {
        var dbContext = await GetDbContextAsync();

        dbContext
            .Set<TaskList>()
            .Remove(taskList);

        return true;
    }
}