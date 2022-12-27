using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.Crud.Commands;

public class BaseCommand<TValidation> where TValidation : IValidator, new()
{
    [NotMapped] public virtual IList<ValidationFailure> Errors => Validate()?.Errors;

    public ValidationResult Validate()
    {
        var validation = new TValidation();
        var context = new ValidationContext<object>(this);
        var validationResult = validation.Validate(context);
        return validationResult;
    }
}