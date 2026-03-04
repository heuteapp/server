using HeuteApp.Api.Models.Public.Responses.Layout;
using HeuteApp.Application.Results.Layout;

namespace HeuteApp.Api.Mappers.Layout;

public static class LayoutResponseMapper
{
    public static PublicHeuteLayoutResponse ToPublicResponse(
        this HeuteLayoutResult result)
    {
        return new PublicHeuteLayoutResponse(
            result.Name,
            result.Version,
            [.. result.Sections.Select(s => s.ToPublicResponse())]
        );
    }

    public static PublicLayoutSectionResponse ToPublicResponse(
        this LayoutSectionResult section)
    {
        return new PublicLayoutSectionResponse(
            section.Name,
            section.Area
        );
    }
}