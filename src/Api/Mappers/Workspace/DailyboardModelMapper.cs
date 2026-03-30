using HeuteApp.Api.Models.Requests.Workspace.Dailyboard;
using HeuteApp.Api.Models.Responses.Workspace.Dailyboard;
using HeuteApp.Application.Results.Dailyboard;
using HeuteApp.Core.Enums.Commands;
using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Domain.Dailyboard;
using HeuteApp.Core.Mappers.Commands.Payloads;
using HeuteApp.Application.Services.Internal;
using HeuteApp.Application.Mappers;

namespace HeuteApp.Api.Mappers.Workspace;

public static class DailyboardModelMapper
{
    public static DailyboardCommand ToDomain(this DailyboardCommandRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.Type switch
        {
            DailyboardCommandType.CreateCard => new CreateCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                DailyboardCommandPayloadsMapper.HandleCreateCardPayload(request.Payload)
            ),
            DailyboardCommandType.PlaceCard => new PlaceCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                DailyboardCommandPayloadsMapper.HandlePlaceCardPayload(request.Payload)
            ),
            DailyboardCommandType.DeleteCard => new DeleteCardCommand(
                DateTimeOffset.Parse(request.OccurredAt),
                DailyboardCommandPayloadsMapper.HandleDeleteCardPayload(request.Payload)
            ),
            _ => throw new NotSupportedException($"Event type not supported: {request.Type}")
        };
    }

    //

    public static async Task<DailyboardResponse> ToResponse(this DailyboardResult dailyboard, InternalLayoutService layoutService)
    {
        ArgumentNullException.ThrowIfNull(dailyboard);
        ArgumentNullException.ThrowIfNull(layoutService);

        var layout = await layoutService.GetLayoutByIdAsync(dailyboard.LayoutId)
            ?? throw new Exception("Layout not found.");

        return new DailyboardResponse(
            Date: dailyboard.Date,
            Layout: layout.ToResult().ToResponse(),
            Cards: [.. dailyboard.Cards.Select(ToResponse)]
        );
    }

    public static DailyboardCardResponse ToResponse(this DailyboardCardResult card)
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