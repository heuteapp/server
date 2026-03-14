using System.Text.Json;
using HeuteApp.Core.Commands.Payloads.Board;

namespace HeuteApp.Core.Mappers.Commands.Payloads;

public static class BoardCommandPayloadsMapper
{
    public static CreateCardPayload HandleCreateCardPayload(JsonElement payload)
    {
        var definitionElement = payload.GetProperty("definition");

        return new CreateCardPayload(
            Definition: new(
                definitionElement.GetProperty("name").GetString()!,
                definitionElement.GetProperty("title").GetString(),
                definitionElement.GetProperty("sectionName").GetString(),
                definitionElement.GetProperty("colIndex").GetInt32(),
                definitionElement.GetProperty("rowIndex").GetInt32(),
                definitionElement.GetProperty("rowIndex").GetInt32(),
                definitionElement.GetProperty("rowIndex").GetInt32()
            )
        );
    }

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

    public static DeleteCardPayload HandleDeleteCardPayload(JsonElement payload)
    {
        var keyElement = payload.GetProperty("key");

        return new DeleteCardPayload(
            Key: new(
                keyElement.GetProperty("name").GetString()!
            )
        );
    }
}