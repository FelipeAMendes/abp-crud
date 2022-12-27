using Abp.Crud.Models;
using Abp.Crud.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Abp.Crud.Web.Pages.Tasks;

public class DetailsModel : PageModel
{
    private readonly ITaskListAppService _taskListService;

    public DetailsModel(ITaskListAppService taskListService)
    {
        _taskListService = taskListService;
    }

    public TaskListModel TaskList { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var taskList = await _taskListService.GetByIdAsync(id.Value);
        if (taskList is null)
            return NotFound();

        TaskList = taskList;
        return Page();
    }
}