#pragma warning disable CA1822
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Services;

public class BoardPlacementService
{
    public bool IsFitsInSection(HeuteLayout layout, Guid sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(layout);

        var section = layout.Sections.FirstOrDefault(s => s.Id == sectionId);
        if (section is null)
            return false;

        return section.Size.Contains(position);
    }

    public BoardCard? GetOverlappingCard(HeuteBoard board, Guid? cardId, Guid sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(board);

        return board.Cards.FirstOrDefault(c =>
            (cardId == null || c.Id != cardId) &&
            c.SectionId == sectionId &&
            c.Position?.Overlaps(position) == true);
    }

    public void EnsureFitsInSection(HeuteLayout layout, Guid sectionId, GridRect position)
    {
        if (!IsFitsInSection(layout, sectionId, position))
            throw new InvalidOperationException("Card Position does not fit in section");
    }

    public void EnsureNoOverlap(HeuteBoard board, Guid? cardId, Guid sectionId, GridRect position)
    {
        var overlappingCard = GetOverlappingCard(board, cardId, sectionId, position);
        if (overlappingCard is not null)
            throw new InvalidOperationException("Card Position overlaps with another card");
    }
}