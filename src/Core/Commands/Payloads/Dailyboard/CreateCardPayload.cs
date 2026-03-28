using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Core.Commands.Payloads.Dailyboard;

public record CreateCardPayload(
    DailyboardCardDefinition Definition
);