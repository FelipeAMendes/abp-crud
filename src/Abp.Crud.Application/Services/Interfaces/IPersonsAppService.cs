using Abp.Crud.Entities;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp.Crud.Services.Interfaces;

public interface IPersonsAppService
{
    Task<PagedResultDto<Person>> GetListAsync(PagedAndSortedResultRequestDto request);
}