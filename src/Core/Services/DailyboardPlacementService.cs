#pragma warning disable CA1822
using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.Aggregates.Layout;
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Core.Services;

public class DailyboardPlacementService
{
    public DailyboardCard AddCard(HeuteDailyboard dailyboard, HeuteLayout layout, DailyboardCardDefinition definition)
    {        
        ArgumentNullException.ThrowIfNull(dailyboard);

        if(dailyboard.IsMaxCardsReached())
            throw new InvalidOperationException($"Dailyboard cannot have more than {HeuteDailyboard.MaxCardCount} cards");

        ArgumentNullException.ThrowIfNull(definition);

        var card = dailyboard.Internal_CreateCard(definition);

        if(card.Placement is not null)
        {
            EnsureFitsInSection(layout, card.Placement);
            EnsureNoOverlap(dailyboard, new(card.Name), card.Placement);
        }
            
        dailyboard.Internal_AddCard(card);
        return card;
    }

    public DailyboardCard PlaceCard(HeuteDailyboard dailyboard, HeuteLayout layout, DailyboardCardKey cardKey, DailyboardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(dailyboard);
        ArgumentNullException.ThrowIfNull(layout);
        ArgumentNullException.ThrowIfNull(placement);

        var card = dailyboard.Cards.FirstOrDefault(c => c.Name == cardKey.Name) 
            ?? throw new InvalidOperationException("Card not found");

        EnsureFitsInSection(layout, placement);
        EnsureNoOverlap(dailyboard, cardKey, placement);

        return card;
    }

    public DailyboardCard DeleteCard(HeuteDailyboard dailyboard, HeuteLayout layout, DailyboardCardKey cardKey)
    {
        ArgumentNullException.ThrowIfNull(dailyboard);
        ArgumentNullException.ThrowIfNull(layout);
  
        var card = dailyboard.Internal_RemoveCard(cardKey) 
            ?? throw new InvalidOperationException("Card not found");

        return card;
    }

    public bool IsFitsInSection(HeuteLayout layout, DailyboardCardPlacement placement)
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

    public DailyboardCard? GetOverlappingCard(HeuteDailyboard dailyboard, DailyboardCardKey cardKey, DailyboardCardPlacement placement)
    {
        ArgumentNullException.ThrowIfNull(dailyboard);
        ArgumentNullException.ThrowIfNull(placement);

        foreach (var card in dailyboard.Cards)
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

    public void EnsureFitsInSection(HeuteLayout layout, DailyboardCardPlacement placement)
    {
        if (!IsFitsInSection(layout, placement))
            throw new InvalidOperationException($"Card Position does not fit in section: \n{placement}");
    }

    public void EnsureNoOverlap(HeuteDailyboard dailyboard, DailyboardCardKey cardKey, DailyboardCardPlacement placement)
    {
        var overlappingCard = GetOverlappingCard(dailyboard, cardKey, placement);
        if (overlappingCard is not null)
            throw new InvalidOperationException($"Card Position overlaps with another card: {overlappingCard.Placement}");
    }
}