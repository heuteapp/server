using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(string id)
{
    private readonly Rect? m_rect = null;

    private readonly GridSize? m_size = null;

    //
    
    public string Id => id;

    public Rect? Rect => m_rect;

    public GridSize? Size => m_size;
}