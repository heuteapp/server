using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardMapper
{
    public static HeuteBoard ToDomainModel(this BoardEntity entity)
    {
        return HeuteBoard.FromSnapshot(entity.ToSnapshot());
    }

    public static HeuteBoardSnapshot ToSnapshot(this BoardEntity entity)
    {
        return new HeuteBoardSnapshot(
            entity.Id,
            entity.OwnerId,
            entity.LayoutId,
            entity.Date,
            new HeuteBoardProps(
                entity.Cards.Select(c => c.ToSnapshot())
            )
        );
    }   
}