using HeuteApp.Api.Models.Responses.Workspace.Layout;
using HeuteApp.Application.Results.Layout;

namespace HeuteApp.Api.Mappers.Workspace;

public static class LayoutMapper
{
    public static LayoutResponse ToResponse(this LayoutResult layout)
        => new(
            Name: layout.Name,
            Version: layout.Version,
            ColCount: layout.Dimensions.ColCount,
            RowCount: layout.Dimensions.RowCount,
            Sections: [.. layout.Sections.Select(s => s.ToResponse())]
        );

    public static LayoutSectionResponse ToResponse(this LayoutSectionResult section)
        => new(
            ColIndex: section.Position.ColIndex,
            RowIndex: section.Position.RowIndex,
            ColSpan: section.Position.ColSpan,
            RowSpan: section.Position.RowSpan
        );
}