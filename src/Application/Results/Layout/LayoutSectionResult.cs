namespace HeuteApp.Application.Results.Layout;

public record LayoutSectionResult(
    Guid Id,
    string Name,
    int X,
    int Y,
    int Width,
    int Height,
    int ColCount,
    int RowCount);