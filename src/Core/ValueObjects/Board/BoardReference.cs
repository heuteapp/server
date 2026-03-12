namespace HeuteApp.Core.ValueObjects.Board;

public sealed record BoardReference
{    
    public Guid LayoutId { get; }

    //

    private BoardReference() { }

    public BoardReference(Guid layoutId)
    {
        LayoutId = layoutId;
    }
}