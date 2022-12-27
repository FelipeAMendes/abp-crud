using Abp.Crud.Commands.TaskListCommands;
using Abp.Crud.Models;
using Abp.Crud.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;

namespace Abp.Crud.Web.Pages.Tasks;

public class IndexModel : PageModel
{
    private readonly ITaskListAppService _taskListService;
    
    public IndexModel(ITaskListAppService taskListService)
    {
        _taskListService = taskListService;
    }

    [BindProperty(SupportsGet = true)]
    public string Title { get; set; }

    [BindProperty(SupportsGet = true, Name = "currentPage")]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string Sort { get; set; }

    [BindProperty(SupportsGet = true)]
    public int TotalCount { get; set; }

    public IList<TaskListModel> TaskList { get; set; } = default!;
    public PagerModel PagerModel { get; set; }

    public async Task OnGetAsync()
    {
        var filter = new FilterTaskListCommand
        {
            CurrentPage = CurrentPage,
            Title = Title,
            OrderBy = Sort,
            PageSize = 10
        };

        var pagedResult = await _taskListService.GetPaginatedAsync(filter);
        PagerModel = new PagerModel(pagedResult.Total, pagedResult.Pages, CurrentPage, pagedResult.PageSize, "/tasks", Sort);
        TaskList = pagedResult.Items.ToList();
    }
}