#pragma warning disable CA1822
using HeuteApp.Core.ValueObjects;
using HeuteApp.Core.Aggregates.Board;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Services;

public class BoardPlacementService
{
    public void EnsureFitsInSection(HeuteLayout layout, Guid sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(layout);

        var section = layout.Sections.FirstOrDefault(s => s.Id == sectionId)
            ?? throw new InvalidOperationException(
                $"Section with id {sectionId} does not exist.");

        if (!section.Size.Contains(position))
            throw new InvalidOperationException(
                $"Position {position} does not fit within section size {section.Size}.");
    }

    public void EnsureNoOverlap(HeuteBoard board, Guid? cardId, Guid sectionId, GridRect position)
    {
        var conflict = board.Cards.FirstOrDefault(c =>
            (cardId == null || c.Id != cardId) &&
            c.SectionId == sectionId &&
            c.Position?.Overlaps(position) == true);

        if (conflict is not null)
            throw new InvalidOperationException($"Position overlaps with card {conflict.Id}");
    }
}