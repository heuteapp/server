using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Commands.Payloads.Board;

public record PlaceCardCommandPayload(
    BoardCardKey CardKey,
    BoardCardPlacement Placement
);