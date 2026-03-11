using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Application.Results.Board;

public sealed record BoardCardResult(
    Guid Id,
    BoardCardContent? Content,
    BoardCardPlacement? Placement);