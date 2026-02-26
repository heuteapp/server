using HeuteApp.Core.Entities;
using HeuteApp.Infrastructure.Persistence.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardMapper
{
    public static HeuteBoard ToDomainModel(this BoardEntity entity)
    {
        return HeuteBoard.FromProps(entity.Id, entity.OwnerId, entity.LayoutId, entity.Date, entity.ToProps());
    }

    public static HeuteBoardSnapshot ToSnapshot(this BoardEntity entity)
    {
        return new HeuteBoardSnapshot(
            entity.Id,
            entity.OwnerId,
            entity.LayoutId,
            entity.Date,
            entity.ToProps()
        );
    }   

    public static HeuteBoardProps ToProps(this BoardEntity entity)
    {
        return new HeuteBoardProps(
            [.. entity.Cards.Select(c => c.ToSnapshot())]
        );
    }

    public static BoardEntity ToEntity(this HeuteBoard board)
    {
        return new BoardEntity
        {
            Id = board.Id,
            OwnerId = board.OwnerId,
            LayoutId = board.LayoutId,
            Date = board.Date,
            Cards = [.. board.Cards.Select(c => c.ToEntity(board.Id))]
        };
    }
}