namespace HeuteApp.Application.Models.Result;

public sealed record LayoutResult(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    IReadOnlyCollection<LayoutSectionResult> Sections);

public sealed record LayoutSectionResult(
    Guid Id,
    string Name,
    int X,
    int Y,
    int Width,
    int Height,
    int ColCount,
    int RowCount);