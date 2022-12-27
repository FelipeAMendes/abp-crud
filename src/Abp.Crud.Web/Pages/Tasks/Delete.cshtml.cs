using Abp.Crud.Models;
using Abp.Crud.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Abp.Crud.Web.Pages.Tasks;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ITaskListAppService _taskListService;

    public DeleteModel(ITaskListAppService taskListService)
    {
        _taskListService = taskListService;
    }

    [BindProperty]
    public TaskListModel TaskList { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
            return NotFound();

        var taskList = await _taskListService.GetByIdAsync(id.Value);
        if (taskList is null)
            return NotFound();

        TaskList = taskList;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var taskList = await _taskListService.GetByIdAsync(id.Value);
        if (taskList is null)
            return RedirectToPage("./Index");

        await _taskListService.DeleteAsync(id.Value);
        return RedirectToPage("./Index");
    }
}