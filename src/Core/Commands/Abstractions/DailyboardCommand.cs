using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Interfaces.Events;

namespace HeuteApp.Core.Commands.Abstractions;

public abstract record DailyboardCommand(
    DateTimeOffset OccurredAt,
    DailyboardCommandType Type
) : IDomainEvent;