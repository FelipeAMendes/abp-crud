using Abp.Crud.Entities;
using Abp.Crud.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Abp.Crud.Services;

public class PersonsAppService : BaseAppService, IPersonsAppService
{
    private readonly IRepository<Person, int> _repository;

    public PersonsAppService(IRepository<Person, int> repository)
    {
        _repository = repository;
    }

    public async Task<PagedResultDto<Person>> GetListAsync(PagedAndSortedResultRequestDto request)
    {
        var queryable = await _repository.GetQueryableAsync();

        var query = queryable
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount);

        query = !string.IsNullOrEmpty(request.Sorting)
            ? query.OrderBy(request.Sorting)
            : query.OrderByDescending(o => o.BirthDate);

        var countRows = await queryable.CountAsync();

        var items = await query.ToListAsync();

        return new PagedResultDto<Person> { Items = items, TotalCount = countRows };
    }
}
