using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Commands.Payloads.Board;

public record DeleteCardPayload(
    BoardCardKey Key
);