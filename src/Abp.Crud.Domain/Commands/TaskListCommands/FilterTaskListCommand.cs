using Abp.Crud.Entities;
using FluentValidation;

namespace Abp.Crud.Commands.TaskListCommands;

public class FilterTaskListCommand : BaseCommand<FilterCategoryQueriesValidation>
{
    public const int LimitPerPage = 10;
    public const string FieldsOrderBy = "Id, Title";

    public int CurrentPage { get; set; }
    public string Title { get; set; }
    public int? PageSize { get; set; }
    public string OrderBy { get; set; } = "Id DESC";
}

public class FilterCategoryQueriesValidation : AbstractValidator<FilterTaskListCommand>
{
    public FilterCategoryQueriesValidation()
    {
        RuleFor(x => x.Title).MaximumLength(TaskListSpecification.TitleColumnSize);
        RuleFor(x => x.CurrentPage).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(FilterTaskListCommand.LimitPerPage);
    }
}