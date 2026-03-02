namespace HeuteApp.Api.Models.Public.Responses.Layout;

public sealed record PublicLayoutSectionResponse(
    string Name,
    int X,
    int Y,
    int Width,
    int Height,
    int ColCount,
    int RowCount);