using System.Text.Json;
using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Application.Results.Board;
using HeuteApp.Application.Services;
using HeuteApp.Core.Enums.Events;
using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Events.Domain.Board;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Api.Mappers.Workspace;

public static class BoardModelMapper
{
    public static BoardEvent ToDomain(this BoardEventRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.Type switch
        {
            BoardEventType.CardCreated => new CardCreatedEvent(
                DateTimeOffset.Parse(request.OccurredAt),
                JsonSerializer.Deserialize<BoardCardDefinition>(request.Payload.ToString()!)!
            ),
            
            _ => throw new NotSupportedException($"Event type not supported: {request.Type}")
        };
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