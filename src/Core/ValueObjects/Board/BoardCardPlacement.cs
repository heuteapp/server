using HeuteApp.Core.ValueObjects.Layout;

namespace HeuteApp.Core.ValueObjects.Board;

public sealed class BoardCardPlacement
{
    public LayoutSectionKey Section { get; private set; } = null!;
    
    public GridRect Position { get; private set; } = null!;

    private BoardCardPlacement() { }

    public BoardCardPlacement(LayoutSectionKey section, GridRect position)
    {
        Section = section;
        Position = position;
    }
}