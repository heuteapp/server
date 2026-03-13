using HeuteApp.Core.Commands.Abstractions;
using HeuteApp.Core.Commands.Contexts;
using HeuteApp.Core.Commands.Domain.Board;
using HeuteApp.Core.Services;

namespace HeuteApp.Core.Commands.Dispatchers;

public class BoardCommandDispatcher(
    BoardPlacementService boardPlacementService)
{
    public void Dispatch(BoardCommandContext context, IReadOnlyCollection<BoardCommand> events)
    {
        foreach (var boardEvent in events)
        {
            HandleEvent(context, boardEvent);
        }
    }

    private void HandleEvent(BoardCommandContext context, BoardCommand boardEvent)
    {
        switch (boardEvent)
        {
            case CreateCardCommand e:
                HandleCardCreatedEvent(context, e);
                break;
            default:
                throw new NotSupportedException($"Event type {boardEvent.Type} is not supported.");
        }
    }

    //

    private void HandleCardCreatedEvent(BoardCommandContext context, CreateCardCommand e)
    {
        boardPlacementService.AddCard(context.Board, context.Layout, e.Payload);
    }
}