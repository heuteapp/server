using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Api.Models.Public.Request;

public record CreateLayoutRequest(string Name, LayoutProps Props);