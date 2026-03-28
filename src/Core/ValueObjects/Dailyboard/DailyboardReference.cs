namespace HeuteApp.Core.ValueObjects.Dailyboard;

public sealed record DailyboardReference
{    
    public Guid LayoutId { get; private set; } = Guid.Empty;

    //

    public DailyboardReference() { }

    public DailyboardReference(Guid layoutId)
    {
        LayoutId = layoutId;
    }
}