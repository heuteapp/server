using HeuteApp.Core.ValueObjects.Dailyboard;

namespace HeuteApp.Core.Aggregates.Dailyboard;

public class HeuteDailyboard
{
    public static int MaxCardCount => 12;

    //

    private readonly List<DailyboardCard> m_cards = [];

    protected HeuteDailyboard() { }

    protected HeuteDailyboard(DailyboardOwnership ownership, DailyboardReference reference, DailyboardDefinition definition)
    {
        Id = Guid.NewGuid();
        OwnerId = ownership.OwnerId;
        CategoryId = ownership.CategoryId;
        LayoutId = reference.LayoutId;
        Date = definition.Date;
    }

    //

    public static HeuteDailyboard Create(DailyboardOwnership ownership, DailyboardReference reference, DailyboardDefinition definition)
    {
        return new HeuteDailyboard(ownership, reference, definition);
    }

    //

    public Guid Id { get; private set; } = Guid.Empty;

    public Guid OwnerId { get; private set; } = Guid.Empty;

    public Guid LayoutId { get; private set; } = Guid.Empty;

    public Guid CategoryId { get; private set; } = Guid.Empty;

    public DateOnly Date { get; private set; } = DateOnly.MinValue;

    public IReadOnlyCollection<DailyboardCard> Cards => m_cards;

    //

    public bool IsMaxCardsReached()
    {
        return Cards.Count >= MaxCardCount;
    }

    //

    internal protected virtual DailyboardCard Internal_CreateCard(DailyboardCardDefinition definition)
    {
        return DailyboardCard.Create(definition);
    }

    internal bool Internal_AddCard(DailyboardCard card)
    {
        ArgumentNullException.ThrowIfNull(card);

        if (m_cards.Any(c => c.Name == card.Name))
            return false;

        m_cards.Add(card);
        return true;
    }

    internal DailyboardCard? Internal_RemoveCard(DailyboardCardKey cardKey)
    {
        var card = Cards.First(c => c.Name == cardKey.Name);
        m_cards.Remove(card);
        return card;
    }
}