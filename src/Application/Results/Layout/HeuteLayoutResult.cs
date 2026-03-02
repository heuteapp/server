namespace HeuteApp.Application.Results.Layout;

public record HeuteLayoutResult(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    IReadOnlyList<LayoutSectionResult> Sections);
    