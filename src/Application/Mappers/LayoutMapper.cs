using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Results.Layout;

namespace HeuteApp.Application.Mappers;

public static class LayoutMapper
{
    public static HeuteLayoutResult ToResult(this HeuteLayout layout)
    {
        ArgumentNullException.ThrowIfNull(layout);
        
        return new HeuteLayoutResult(
            layout.Id,
            layout.OwnerId,
            layout.Name,
            layout.Version,
            [..layout.Sections.Select(ToResult)]
        );
    }

    public static LayoutSectionResult ToResult(this LayoutSection section)
    {    
        ArgumentNullException.ThrowIfNull(section);
        
        return new LayoutSectionResult(
            section.Id,
            section.Name,
            section.Area
        );
    }
}