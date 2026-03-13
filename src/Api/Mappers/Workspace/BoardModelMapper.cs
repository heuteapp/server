using System.Text.Json;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Application.Results.Board;
using HeuteApp.Application.Services;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Domain.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static BoardCommand ToDomain(this BoardCommandRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.Type switch
        {
            BoardCommandType.CreateCard => new CreateCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                HandleCardDefinition(request.Payload)
            ),
            
            _ => throw new NotSupportedException($"Event type not supported: {request.Type}")
        };
    }

    public static BoardCardDefinition HandleCardDefinition(object payload)
    {
        ArgumentNullException.ThrowIfNull(payload);

        if(payload is not JsonElement jsonElement)
            throw new ArgumentException("Payload must be a JsonElement.", nameof(payload));

        return new BoardCardDefinition(
            jsonElement.GetProperty("name").GetString()!,
            jsonElement.GetProperty("title").GetString(),
            jsonElement.GetProperty("sectionName").GetString(),
            jsonElement.GetProperty("colIndex").GetInt32(),
            jsonElement.GetProperty("rowIndex").GetInt32(),
            jsonElement.GetProperty("colSpan").GetInt32(),
            jsonElement.GetProperty("rowSpan").GetInt32()
        );
    }

    //

    public static async Task<BoardResponse> ToResponse(this BoardResult board, LayoutService layoutService)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(layoutService);

        var layout = await layoutService.GetLayoutByIdAsync(board.LayoutId)
            ?? throw new Exception("Layout not found.");

        return new BoardResponse(
            Date: board.Date,
            Layout: layout.ToResponse(),
            Cards: [.. board.Cards.Select(ToResponse)]
        );
    }

    public static BoardCardResponse ToResponse(this BoardCardResult card)
        => new(
            Name: card.Name,
            Title: card.Content?.Title,
            SectionName: card.Placement?.SectionName,
            ColIndex: card.Placement?.ColIndex,
            RowIndex: card.Placement?.RowIndex,
            ColSpan: card.Placement?.ColSpan,
            RowSpan: card.Placement?.RowSpan
        );
}