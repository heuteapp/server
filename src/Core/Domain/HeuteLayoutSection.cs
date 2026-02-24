namespace HeuteApp.Core.Domain;

public class HeuteLayoutSection(string id)
{
    private readonly string m_id = id;

    //
    
    public string Id => m_id;
}