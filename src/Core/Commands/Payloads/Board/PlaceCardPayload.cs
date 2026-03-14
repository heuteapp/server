using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Commands.Payloads.Board;

public record PlaceCardPayload(
    BoardCardKey CardKey,
    BoardCardPlacement Placement
);