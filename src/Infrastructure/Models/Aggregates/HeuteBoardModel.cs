using HeuteApp.Core.Aggregates;
using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Models.Aggregates;

public class HeuteBoardModel : HeuteBoard
{
    protected override BoardCard OnCreateCard(Guid id, BoardCardProps props)
    {
        return new BoardCardModel(id, props);
    }
}