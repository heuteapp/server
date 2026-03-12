namespace HeuteApp.Api.Models.Responses.Workspace.Layout;

using System.Collections.Generic;

public record LayoutResponse(
    int ColCount,
    int RowCount,
    IEnumerable<LayoutSectionResponse> Sections
);