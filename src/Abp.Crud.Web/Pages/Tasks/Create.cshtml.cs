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
public class CreateModel : AbpPageModel
{
    private readonly ITaskListAppService _taskListService;
    private readonly IStringLocalizer<Resource> _stringLocalizer;

    public CreateModel(ITaskListAppService taskListService, IStringLocalizer<Resource> stringLocalizer)
    {
        _taskListService = taskListService;
        _stringLocalizer = stringLocalizer;
    }

    public IActionResult OnGet()
    {
        LoadStatus();
        return Page();
    }

    public SelectList StatusSelectList { get; set; }

    [BindProperty]
    public TaskListModel TaskList { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        LoadStatus();

        if (!ModelState.IsValid)
            return Page();

        var result = await _taskListService.CreateAsync(TaskList);
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
        StatusSelectList = new SelectList(status.Select(x => new SelectListModel((int)x, _stringLocalizer[$"TaskList:{x}"])), "Id", "Name");
    }
}