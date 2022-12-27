using Abp.Crud.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Abp.Crud.DbMigrator;

public class TaskListDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<TaskList, Guid> _taskListRepository;

    public TaskListDataSeedContributor(IRepository<TaskList, Guid> taskListRepository)
    {
        _taskListRepository = taskListRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _taskListRepository.GetCountAsync() > 0)
            return;

        var tasks = GetTaskList(50);

        await _taskListRepository.InsertManyAsync(tasks);
    }

    private static IEnumerable<TaskList> GetTaskList(int count)
    {
        var task = new Faker<TaskList>()
            .CustomInstantiator(x =>
            {
                var id = Guid.NewGuid();
                var title = x.Lorem.Sentence(3);
                var assignedTo = x.Person.FullName;
                var dueDate = x.Date.Between(DateTime.Today, DateTime.Today.AddDays(10));
                var status = x.Random.Enum<TaskListStatus>();

                return new TaskList(id, title, assignedTo, status, dueDate);
            });

        return task.Generate(count);
    }
}