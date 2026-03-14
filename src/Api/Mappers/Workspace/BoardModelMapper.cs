using HeuteApp.Api.Models.Requests.Workspace.Board;
using HeuteApp.Api.Models.Responses.Workspace.Board;
using HeuteApp.Application.Results.Board;
using HeuteApp.Application.Services;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Domain.Board;
using HeuteApp.Core.Mappers.Commands.Payloads;

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
                BoardCommandPayloadsMapper.HandleCreateCardPayload(request.Payload)
            ),
            BoardCommandType.PlaceCard => new PlaceCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                BoardCommandPayloadsMapper.HandlePlaceCardPayload(request.Payload)
            ),
            BoardCommandType.DeleteCard => new DeleteCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                BoardCommandPayloadsMapper.HandleDeleteCardPayload(request.Payload)
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