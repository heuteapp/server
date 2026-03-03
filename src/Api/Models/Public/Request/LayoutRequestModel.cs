using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Api.Models.Public.Request;

public record CreateLayoutRequest(LayoutKey Key, LayoutProps Props);