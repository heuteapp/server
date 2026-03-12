using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Application.Results.Layout;

namespace HeuteApp.Application.Mappers;

public static class LayoutMapper
{
    public static LayoutResult ToResult(this HeuteLayout layout)
    {
        ArgumentNullException.ThrowIfNull(layout);
        
        return new LayoutResult(
            layout.Id,
            layout.OwnerId,
            layout.Name,
            layout.Version,
            layout.Dimensions,
            [..layout.Sections.Select(ToResult)]
        );
    }

    public static LayoutSectionResult ToResult(this LayoutSection section)
    {    
        ArgumentNullException.ThrowIfNull(section);
        
        return new LayoutSectionResult(
            section.Id,
            section.Name,
            section.Position
        );
    }
}