namespace HeuteApp.Api.Models.Request;

public record GetLayoutRequest(Guid OwnerId, string Name, int Version);