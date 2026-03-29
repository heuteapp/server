using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Application.Results.Layout;

public record LayoutResult(
    Guid Id,
    Guid? UserId,
    string Name,
    int Version,
    GridDimensions Dimensions,
    IReadOnlyList<LayoutSectionResult> Sections);