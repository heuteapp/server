using System.Text.Json;
using HeuteApp.Core.Commands.Payloads.Board;

namespace HeuteApp.Core.Mappers.Commands.Payloads;

public static class BoardCommandPayloadsMapper
{
    public static PlaceCardPayload HandlePlaceCardPayload(JsonElement payload)
    {
        var keyElement = payload.GetProperty("key");
        var placementElement = payload.GetProperty("placement");

        return new PlaceCardPayload(
            Key: new(
                keyElement.GetProperty("name").GetString()!
            ),
            Placement: new(
                placementElement.GetProperty("sectionName").GetString()!,
                placementElement.GetProperty("colIndex").GetInt32(),
                placementElement.GetProperty("rowIndex").GetInt32(),
                placementElement.GetProperty("rowIndex").GetInt32(),
                placementElement.GetProperty("rowIndex").GetInt32()
            )
        );
    }
}