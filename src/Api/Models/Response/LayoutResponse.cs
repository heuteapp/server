using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Api.Models.Response;

public record LayoutResponse(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    IEnumerable<LayoutSectionResponse> Sections
);

public record LayoutSectionResponse(
    Guid Id,
    string Name,
    Rect Rect,
    GridSize Size
);