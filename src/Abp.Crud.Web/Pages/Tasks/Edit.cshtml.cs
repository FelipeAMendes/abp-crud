using Abp.Crud.Entities;
using Abp.Crud.Localization;
using Abp.Crud.Models;
using Abp.Crud.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.Crud.Web.Pages.Tasks;

[Authorize]
public class EditModel : AbpPageModel
{
    private readonly ITaskListAppService _taskListService;
    private readonly IStringLocalizer<Resource> _stringLocalizer;
        
    public EditModel(ITaskListAppService taskListService, IStringLocalizer<Resource> stringLocalizer)
    {
        _taskListService = taskListService;
        _stringLocalizer = stringLocalizer;
    }

    public SelectList StatusSelectList { get; set; }

    [BindProperty]
    public TaskListModel TaskList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        LoadStatus();

        if (id is null)
            return NotFound();

        var taskList = await _taskListService.GetByIdAsync(id.Value);
        if (taskList is null)
            return NotFound();

        TaskList = taskList;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        LoadStatus();

        if (!ModelState.IsValid)
            return Page();

        var result = await _taskListService.UpdateAsync(TaskList);
        if (result.Success)
            return RedirectToPage("./Index");

        result.Errors.ToList().ForEach(err =>
        {
            Alerts.Danger(text: err.ErrorMessage, title: "Oops");
        });
        return Page();
    }

    public void LoadStatus()
    {
        var status = Enum.GetValues<TaskListStatus>().ToList();
        StatusSelectList = new SelectList(status.Select(x => new SelectListModel((int)x, _stringLocalizer[$"TaskList:{x}"])), "Id", "Name", TaskList?.Status);
    }
}
