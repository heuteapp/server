using HeuteApp.Application.Models.Result;
using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;

namespace HeuteApp.Application.Mappers;

public static class LayoutMapper
{
    public static LayoutResult ToResult(this HeuteLayout layout)
    {
        return new LayoutResult(
            layout.Id,
            layout.OwnerId,
            layout.Name,
            layout.Version,
            [..layout.Sections.Select(ToResult)]
        );
    }

    public static LayoutSectionResult ToResult(this LayoutSection section)
    {
        return new LayoutSectionResult(
            section.Id,
            section.Name,
            section.Rect.X,
            section.Rect.Y,
            section.Rect.Width,
            section.Rect.Height,
            section.Size.ColCount,
            section.Size.RowCount
        );
    }
}