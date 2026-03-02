using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Api.Models.Response;

public sealed record LayoutResponse(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    IEnumerable<LayoutSectionResponse> Sections) 
{
    public static LayoutResponse FromDomain(Layout layout)
    {
        return new LayoutResponse(
            layout.Id,
            layout.OwnerId,
            layout.Name,
            layout.Version,
            layout.Sections.Select(s => LayoutSectionResponse.FromDomain(s))
        );
    }
}

public sealed record LayoutSectionResponse(
    Guid Id,
    string Name,
    Rect Rect,
    GridSize Size)
{
    public static LayoutSectionResponse FromDomain(LayoutSection section)
    {
        return new LayoutSectionResponse(
            section.Id,
            section.Name,
            section.Rect,
            section.Size
        );
    }
}