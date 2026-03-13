using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Interfaces.Events;

namespace HeuteApp.Core.Commands.Abstractions;

public abstract record BoardCommand(
    DateTimeOffset OccurredAt,
    BoardCommandType Type
) : IDomainEvent;