using Abp.Crud.Handlers;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Abp.Crud.Commands;

public interface ICommandResult
{
    bool Success { get; set; }
    string Message { get; set; }
    IEnumerable<ValidationFailure> Errors { get; }
}

public interface ICommandResult<TCommandResult> : ICommandResult
{
    TCommandResult Result { get; set; }
    ValidationType ValidationType { get; set; }
}