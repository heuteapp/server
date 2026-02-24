using HeuteApp.Core.Domain.ValueObjects;

namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(string id)
{
    private readonly string m_id = id;

    private readonly Rect? m_rect = null;

    //
    
    public string Id => m_id;

    public Rect? Rect => m_rect;
}