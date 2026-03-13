namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardReference
{    
    public Guid LayoutId { get; private set; } = Guid.Empty;

    //

    public BoardReference() { }

    public BoardReference(Guid layoutId)
    {
        LayoutId = layoutId;
    }
}