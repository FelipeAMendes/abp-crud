using System.Collections.Generic;
using FluentValidation.Results;

namespace Abp.Crud.Handlers.Interfaces;

public interface IBaseHandler
{
    IList<ValidationFailure> Errors { get; }
}