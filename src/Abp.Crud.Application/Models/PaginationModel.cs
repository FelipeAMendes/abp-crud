using System.Collections.Generic;
using Abp.Crud.Pagination;

namespace Abp.Crud.Models;

public class PaginationModel
{
    public int LimitPaginationLinks { get; set; } = 10;
    public IPaginateModel<object> List { get; set; }
    public Dictionary<string, string> RouteData { get; set; }
}