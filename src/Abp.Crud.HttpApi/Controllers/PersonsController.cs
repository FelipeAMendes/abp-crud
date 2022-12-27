using Abp.Crud.Entities;
using Abp.Crud.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.Crud.Controllers;

public class PersonsController : BaseController
{
    private readonly IRepository<Person, int> _repository;

    public PersonsController(IRepository<Person, int> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IPaginateModel<Person>> GetAsync(int currentPage, int pageSize = 10)
    {
        var queryable = await _repository.GetQueryableAsync();

        var countToSkip = (currentPage - 1) * pageSize;

        var query = queryable
            .Skip(countToSkip)
            .Take(pageSize);

        var countRows = await queryable.CountAsync();
        var countPages = countRows / pageSize;

        var items = await query.ToListAsync();

        return new PaginateModel<Person>(currentPage, countPages, pageSize, countRows, "", items);
    }
}
