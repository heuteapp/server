namespace HeuteApp.Api.Models.Responses.Layout;

public record LayoutSectionResponse(
    string Name,
    int ColIndex,
    int RowIndex,
    int ColSpan,
    int RowSpan
);