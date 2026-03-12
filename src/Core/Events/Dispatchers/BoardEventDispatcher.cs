using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Events.Contexts;
using HeuteApp.Core.Events.Domain.Board;
using HeuteApp.Core.Services;

namespace HeuteApp.Core.Events.Dispatchers;

public class BoardEventDispatcher(
    BoardPlacementService boardPlacementService)
{
    public void Dispatch(BoardEventContext context, IReadOnlyCollection<BoardEvent> events)
    {
        foreach (var boardEvent in events)
        {
            HandleEvent(context, boardEvent);
        }
    }

    private void HandleEvent(BoardEventContext context, BoardEvent boardEvent)
    {
        switch (boardEvent)
        {
            case CardCreatedEvent e:
                HandleCardCreatedEvent(context, e);
                break;
            default:
                throw new NotSupportedException($"Event type {boardEvent.Type} is not supported.");
        }
    }

    //

    private void HandleCardCreatedEvent(BoardEventContext context, CardCreatedEvent e)
    {
        boardPlacementService.AddCard(context.Board, context.Layout, e.Definition);
    }
}