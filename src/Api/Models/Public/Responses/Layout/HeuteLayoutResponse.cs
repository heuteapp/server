namespace HeuteApp.Api.Models.Public.Responses.Layout;

public record HeuteLayoutResponse(
    Guid Id,
    Guid OwnerId,
    string Name,
    int Version,
    IReadOnlyList<LayoutS> Sections);