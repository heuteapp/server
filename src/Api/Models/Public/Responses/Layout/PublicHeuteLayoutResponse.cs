namespace HeuteApp.Api.Models.Public.Responses.Layout;

public sealed record PublicHeuteLayoutResponse(
    string Name,
    int Version,
    IReadOnlyList<PublicLayoutSectionResponse> Sections);