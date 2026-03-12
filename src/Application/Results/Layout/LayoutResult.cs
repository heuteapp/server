using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Application.Results.Layout;

public record LayoutResult(
    Guid Id,
    Guid? OwnerId,
    string Name,
    int Version,
    GridDimensions Dimensions,
    IReadOnlyList<LayoutSectionResult> Sections);