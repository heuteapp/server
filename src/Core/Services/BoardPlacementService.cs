#pragma warning disable CA1822
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects;
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

        if(card.Placement is not null)
        {
            EnsureFitsInSection(layout, card.Placement);
            EnsureNoOverlap(board, new(card.Name), card.Placement);
        }
            
        board.Internal_AddCard(card);
        return card;
    }

    public void PlaceCard(HeuteBoard board, HeuteLayout layout, BoardCardKey cardKey, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(placement);

        var card = board.Cards.FirstOrDefault(c => c.Name == cardKey.Name) 
            ?? throw new InvalidOperationException("Card not found");

        EnsureFitsInSection(layout, placement);
        EnsureNoOverlap(board, cardKey, placement);
    }

    public bool IsFitsInSection(HeuteLayout layout, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(placement);

        var section = layout.Sections.FirstOrDefault(s => s.Name == placement.SectionName);
        if (section is null)
            return false;

        var localPosition = new GridRect(
            1, 1,
            section.Position.ColSpan,
            section.Position.RowSpan
        );

        return localPosition.Contains(placement.Position);
    }

    public BoardCard? GetOverlappingCard(HeuteBoard board, BoardCardKey cardKey, BoardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(board);
        ArgumentNullException.ThrowIfNull(placement);

        foreach (var card in board.Cards)
        {
            if(!card.IsPlaced)
                continue;

            if (card.Name == cardKey.Name)
                continue;

            if (card.Placement!.SectionName != placement.SectionName)
                continue;

            if (!card.Placement.Position.Overlaps(placement.Position))
                continue;
            
            return card;
        }

        return null;
    }

    public void EnsureFitsInSection(HeuteLayout layout, BoardCardPlacement placement)
    {
        if (!IsFitsInSection(layout, placement))
            throw new InvalidOperationException($"Card Position does not fit in section: \n{placement}");
    }

    public void EnsureNoOverlap(HeuteBoard board, BoardCardKey cardKey, BoardCardPlacement placement)
    {
        var overlappingCard = GetOverlappingCard(board, cardKey, placement);
        if (overlappingCard is not null)
            throw new InvalidOperationException($"Card Position overlaps with another card: {overlappingCard.Placement}");
    }
}