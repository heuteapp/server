using HeuteApp.Application.Enums.Services;

namespace HeuteApp.Application.Interfaces.Services;

public class ConflictAwareCreateOptions : CreateOptions
{
    public CreateConflictBehavior ConflictBehavior { get; init; } = CreateConflictBehavior.Strict;
}