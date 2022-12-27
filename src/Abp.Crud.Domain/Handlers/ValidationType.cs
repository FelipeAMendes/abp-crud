using System.ComponentModel;

namespace Abp.Crud.Handlers;

public enum ValidationType
{
    [Description("Creation Error")] CreationError = 1,
    [Description("Change Error")] ChangeError,
    [Description("Removal Error")] RemovalError,
    [Description("Item Not Found")] ItemNotFound,
    [Description("Execution Error")] ExecutionError,
    [Description("Time Limit Exceeded")] TimeLimitExceeded
}