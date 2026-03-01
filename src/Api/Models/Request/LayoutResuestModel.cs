namespace HeuteApp.Api.Models.Request;

public record GetLayoutRequest(Guid OwnerId, string Name, int Version);

public record GetLayoutsRequest(Guid OwnerId);

public record CreateLayoutRequest(Guid OwnerId, string Name, int Version);