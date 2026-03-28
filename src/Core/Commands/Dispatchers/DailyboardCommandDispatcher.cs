using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Contexts;
using HeuteApp.Core.Commands.Domain.Dailyboard;
using HeuteApp.Core.Services;

namespace HeuteApp.Core.Commands.Dispatchers;

public class DailyboardCommandDispatcher(
    DailyboardPlacementService dailyboardPlacementService)
{
    public void Dispatch(DailyboardCommandContext context, IReadOnlyCollection<DailyboardCommand> events)
    {
        foreach (var dailyboardEvent in events)
        {
            HandleEvent(context, dailyboardEvent);
        }
    }

    private void HandleEvent(DailyboardCommandContext context, DailyboardCommand dailyboardEvent)
    {
        switch (dailyboardEvent)
        {
            case CreateCardCommand e:
                HandleCardCreatedEvent(context, e);
                break;
            case PlaceCardCommand e:
                HandleCardPlacedEvent(context, e);
                break;
            case DeleteCardCommand e:
                HandleCardDeletedEvent(context, e);
                break;
            default:
                throw new NotSupportedException($"Event type {dailyboardEvent.Type} is not supported.");
        }
    }

    //

    private void HandleCardCreatedEvent(DailyboardCommandContext context, CreateCardCommand e)
    {
        dailyboardPlacementService.AddCard(context.Dailyboard, context.Layout, e.Payload.Definition);
    }

    private void HandleCardPlacedEvent(DailyboardCommandContext context, PlaceCardCommand e)
    {
        dailyboardPlacementService.PlaceCard(context.Dailyboard, context.Layout, e.Payload.Key, e.Payload.Placement);
    }

    private void HandleCardDeletedEvent(DailyboardCommandContext context, DeleteCardCommand e)
    {
        dailyboardPlacementService.DeleteCard(context.Dailyboard, context.Layout, e.Payload.Key);
    }
}