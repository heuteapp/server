namespace HeuteApp.Core.Services;

using HeuteApp.Core.Events.Abstractions;
using HeuteApp.Core.Events.Domain.Board;

public class BoardEventService(
    BoardPlacementService boardPlacementService)
{
    public void Publish(BoardEvent boardEvent)
    {
        switch (boardEvent)
        {
            case CardCreatedEvent e:
                HandleCardCreated(e);
                break;
            // diğer eventler
        }
    }

    //

    private void HandleCardCreated(CardCreatedEvent e)
    {
        boardPlacementService.AddCard(e.Board, e.Layout, e.Definition);
    }
}