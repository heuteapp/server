namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardReference
{
    public BoardReference(Guid layoutId)
    {
        LayoutId = layoutId;
    }

    public Guid LayoutId { get; }
}