namespace HeuteApp.Api.Models.Responses.Workspace.Layout;

public record LayoutSectionResponse(
    string Name,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan
);