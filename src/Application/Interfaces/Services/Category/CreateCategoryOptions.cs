using HeuteApp.Application.Enums.Services;

namespace HeuteApp.Application.Interfaces.Services.Category;

public sealed class CreateCategoryOptions : CreateConflictOptions
{
    public ParentNotFoundBehavior ParentNotFoundBehavior { get; init; } = ParentNotFoundBehavior.Throw;
}