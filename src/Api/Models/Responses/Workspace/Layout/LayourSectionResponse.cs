namespace HeuteApp.Api.Models.Responses.Workspace.Layout;

public record LayoutSectionResponse(
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan
);