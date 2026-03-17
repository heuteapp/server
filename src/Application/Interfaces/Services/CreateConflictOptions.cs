using HeuteApp.Application.Enums.Services;

namespace HeuteApp.Application.Interfaces.Services;

public class CreateConflictOptions : CreateOptions
{
    public CreateConflictBehavior ConflictBehavior { get; init; } = CreateConflictBehavior.Strict;
}