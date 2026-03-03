#pragma warning disable CA1822
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Services;

public class BoardPlacementService
{
    public BoardCard AddCard(HeuteBoard board, HeuteLayout layout, BoardCardDefinition definition)
    {        
        ArgumentNullException.ThrowIfNull(board);

        if(board.IsMaxCardsReached())
            throw new InvalidOperationException($"Board cannot have more than {HeuteBoard.MaxCardCount} cards");

        ArgumentNullException.ThrowIfNull(definition);

        var card = board.Internal_CreateCard(definition);

        if(card.CanBePlaced)
        {
            PlaceCard(board, layout, card.Id, card.Placement!);
        }

        board.Internal_AddCard(card);
        return card;
    }

    public void PlaceCard(HeuteBoard board, HeuteLayout layout, Guid cardId, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(placement);

        var card = board.Cards.FirstOrDefault(c => c.Id == cardId) 
            ?? throw new InvalidOperationException("Card not found");

        EnsureFitsInSection(layout, placement);
        EnsureNoOverlap(board, cardId, placement);

        card.DoPlace(placement);
    }

    public bool IsFitsInSection(HeuteLayout layout, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(placement);

        var section = layout.Sections.FirstOrDefault(s => s.Name == placement.Section.Name);
        if (section is null)
            return false;

        return section.Size.Contains(placement.Position);
    }

    public BoardCard? GetOverlappingCard(HeuteBoard board, Guid? cardId, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(placement);

        return board.Cards.FirstOrDefault(c =>
            (cardId == null || c.Id != cardId) &&
            c.Placement?.Section == placement.Section &&
            c.Placement?.Position?.Overlaps(placement.Position) == true);
    }

    public void EnsureFitsInSection(HeuteLayout layout, BoardCardPlacement placement)
    {
        if (!IsFitsInSection(layout, placement))
            throw new InvalidOperationException("Card Position does not fit in section");
    }

    public void EnsureNoOverlap(HeuteBoard board, Guid? cardId, BoardCardPlacement placement)
    {
        var overlappingCard = GetOverlappingCard(board, cardId, placement);
        if (overlappingCard is not null)
            throw new InvalidOperationException("Card Position overlaps with another card");
    }
}