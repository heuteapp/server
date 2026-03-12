using HeuteApp.Api.Models.Responses.Workspace.Layout;
using HeuteApp.Core.ValueObjects.Layout;
using System.Linq;

namespace HeuteApp.Api.Mappers.Workspace;

public static class LayoutMapper
{
    public static LayoutResponse ToResponse(this LayoutProps layout)
        => new(
            ColCount: layout.ColCount,
            RowCount: layout.RowCount,
            Sections: [.. layout.Sections.Select(s => s.ToResponse())]
        );

    public static LayoutSectionResponse ToResponse(this LayoutSectionDefinition section)
        => new(
            ColIndex: section.ColIndex,
            RowIndex: section.RowIndex,
            ColSpan: section.ColSpan,
            RowSpan: section.RowSpan
        );
}