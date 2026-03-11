#pragma warning disable CA1822
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects.Board;

namespace HeuteApp.Core.Services;

public class BoardPlacementService
{
    public void SyncBoard(HeuteBoard board, HeuteLayout layout, BoardProps syncProps)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(syncProps);

        // Remove cards that are not in syncProps and DEFINITON DOES NOT HAVE ID  JUST HAVE NAME
        var cardsToRemove = board.Cards.Where(c => !syncProps.Cards.Any(sc => sc.Name == c.Name)).ToList();
        foreach (var card in cardsToRemove)
        {
            board.Internal_RemoveCard(card.Id);
        }

        // Add or update cards from syncProps
        foreach (var syncCard in syncProps.Cards)
        {            
            var existingCard = board.Cards.FirstOrDefault(c => c.Name == syncCard.Name);
            if (existingCard == null)
            {
                // Add new card
                var newCard = board.Internal_CreateCard(new BoardCardDefinition(new(syncCard.Name), new(syncCard.Content, syncCard.Placement)));
                board.Internal_AddCard(newCard);
            }
        }
    }

    public BoardCard AddCard(HeuteBoard board, HeuteLayout layout, BoardCardDefinition definition)
    {        
        ArgumentNullException.ThrowIfNull(board);

        if(board.IsMaxCardsReached())
            throw new InvalidOperationException($"Board cannot have more than {HeuteBoard.MaxCardCount} cards");

        ArgumentNullException.ThrowIfNull(definition);

        var card = board.Internal_CreateCard(definition);

        if(card.CanBePlaced)
        {
            var cardId = card.Id;
            var placement = card.Placement!;

            EnsureFitsInSection(layout, placement);
            EnsureNoOverlap(board, cardId, placement);
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

        var section = layout.Sections.FirstOrDefault(s => s.Name == placement.SectionName);
        if (section is null)
            return false;

        return section.Area.Contains(placement.Position);
    }

    public BoardCard? GetOverlappingCard(HeuteBoard board, Guid? cardId, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(placement);

        return board.Cards.FirstOrDefault(c =>
            (cardId == null || c.Id != cardId) &&
            c.Placement?.SectionName == placement.SectionName &&
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