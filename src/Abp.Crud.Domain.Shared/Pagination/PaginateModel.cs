using System;
using System.Collections.Generic;

namespace Abp.Crud.Pagination;

public class PaginateModel<TResult> : IPaginateModel<TResult>
{
    public PaginateModel(int currentPage, int pages, int pageSize, long total, string orderBy, IEnumerable<TResult> items)
    {
        if (pages == 0)
            pages = 1;

        CurrentPage = currentPage;
        Pages = pages;
        PageSize = pageSize;
        Total = total;
        OrderBy = orderBy;
        Items = items ?? Array.Empty<TResult>();
    }

    public int CurrentPage { get; set; }
    public int Pages { get; set; }
    public int PageSize { get; set; }
    public long Total { get; set; }
    public string OrderBy { get; set; }
    public IEnumerable<TResult> Items { get; set; }
}