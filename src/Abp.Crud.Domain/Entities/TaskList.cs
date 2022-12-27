using FluentValidation;
using FluentValidation.Results;
using System;
using Volo.Abp.Domain.Entities;

namespace Abp.Crud.Entities;

public class TaskList : BasicAggregateRoot<Guid>, ICrudEntity
{
    public string Title { get; private set; }
    public string AssignedTo { get; private set; }
    public DateTime DueDate { get; private set; }
    public TaskListStatus Status { get; private set; }

    public DateTime CreationTime { get; }
    public DateTime? LastModificationTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletionTime { get; set; }

    public TaskList(Guid id, string title, string assignedTo, TaskListStatus status, DateTime dueDate) : base(id)
    {
        Title = title;
        AssignedTo = assignedTo;
        Status = status;
        DueDate = dueDate;
        CreationTime = DateTime.Now;
    }

    public void Update(string title, string assignedTo, TaskListStatus status, DateTime dueDate)
    {
        Title = title;
        AssignedTo = assignedTo;
        Status = status;
        DueDate = dueDate;
        LastModificationTime = DateTime.Now;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }

    public ValidationResult Validate()
    {
        var validation = new TaskListValidation();
        var validationResult = validation.Validate(this);
        return validationResult;
    }
}

public class TaskListValidation : AbstractValidator<TaskList>
{
    public TaskListValidation()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TaskListSpecification.TitleColumnSize);
        RuleFor(x => x.AssignedTo).MaximumLength(TaskListSpecification.AssignedToColumnSize);
        RuleFor(x => x.Status).NotNull().IsInEnum();
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(DateTime.Today);
    }
}
