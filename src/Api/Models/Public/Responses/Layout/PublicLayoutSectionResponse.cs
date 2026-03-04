using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Api.Models.Public.Responses.Layout;

public sealed record PublicLayoutSectionResponse(
    string Name,
    GridRect Area
);