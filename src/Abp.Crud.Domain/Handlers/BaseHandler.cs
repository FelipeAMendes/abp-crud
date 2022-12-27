using Abp.Crud.Commands;
using Abp.Crud.Handlers.Interfaces;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Abp.Crud.Handlers;

public abstract class BaseHandler : IBaseHandler
{
    private readonly List<ValidationFailure> _errors;

    protected BaseHandler()
    {
        _errors = new List<ValidationFailure>();
    }

    public bool IsValid => _errors.Count == 0;

    public IList<ValidationFailure> Errors => _errors;

    public void AddError(string propertyName, string errorMessage)
    {
        var validationFailure = new ValidationFailure(propertyName, errorMessage);

        _errors.Add(validationFailure);
    }

    public void AddErrors(IList<ValidationFailure> errors)
    {
        if (errors != null)
            _errors.AddRange(errors);
    }

    public ICommandResult<bool> CreateDefaultCommandResult(bool success)
    {
        return CommandResult<bool>.CreateResult(success);
    }

    public ICommandResult<bool> CreateNotFoundCommandResult()
    {
        return CommandResult<bool>.CreateResult(ValidationType.ItemNotFound);
    }

    public ICommandResult<bool> CreateErrorCommandResult(IList<ValidationFailure> errors)
    {
        return CommandResult<bool>.CreateResult(false, errors);
    }

    private ICommandResult<bool> CreateDefaultCommandResult(ValidationType validationType)
    {
        return CommandResult<bool>.CreateResult(validationType);
    }

    public ICommandResult<bool> CreateDefaultCommandResult(bool success, ValidationType validationType)
    {
        return success
            ? CreateDefaultCommandResult(true)
            : CreateDefaultCommandResult(validationType);
    }
}