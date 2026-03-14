using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Commands.Payloads.Board;

public record CreateCardPayload(
    BoardCardDefinition Definition
);