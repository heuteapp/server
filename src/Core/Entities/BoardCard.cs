using HeuteApp.Core.ValueObjects;

namespace HeuteApp.Core.Entities;

public class BoardCard(Guid id, BoardCardProps props)
{
    public Guid Id { get; private set; } = id;

    public string? Title { get; private set; } = props.Title;

    public Guid? SectionId { get; private set; } = props.SectionId;

    public GridRect? Position { get; private set; } = props.Position;

    public bool IsPlaced
    {
        get
        {
            return SectionId != null && Position != null;
        }
    }

    public bool IsVerified { get; private set; } = false;

    //

    internal void DoPlace(Guid sectionId, GridRect position)
    {
        ArgumentNullException.ThrowIfNull(position);

        SectionId = sectionId;
        Position = position;
        IsVerified = false;
    }

    internal void DoUnplace()
    {
        SectionId = null;
        Position = null;
        IsVerified = false;
    }

    internal void DoVerify()
    {
        IsVerified = true;
    }
}

public sealed record BoardCardProps(
    string? Title,
    Guid? SectionId,
    GridRect? Position
);