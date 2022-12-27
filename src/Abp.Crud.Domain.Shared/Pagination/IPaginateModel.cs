using System.Collections.Generic;

namespace Abp.Crud.Pagination;

public interface IPaginateModel<TResult>
{
    int CurrentPage { get; set; }
    int Pages { get; set; }
    int PageSize { get; set; }
    long Total { get; set; }
    string OrderBy { get; set; }
    IEnumerable<TResult> Items { get; set; }
}