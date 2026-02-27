using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardMapper
{
    public static HeuteBoard ToDomainModel(this BoardModel entity)
    {
        return HeuteBoard.FromProps(entity.Id, entity.OwnerId, entity.LayoutId, entity.Date, entity.ToProps());
    }

    public static HeuteBoardSnapshot ToSnapshot(this BoardModel entity)
    {
        return new HeuteBoardSnapshot(
            entity.Id,
            entity.OwnerId,
            entity.LayoutId,
            entity.Date,
            entity.ToProps()
        );
    }   

    public static HeuteBoardProps ToProps(this BoardModel entity)
    {
        return new HeuteBoardProps(
            [.. entity.Cards.Select(c => c.ToSnapshot())]
        );
    }

    public static BoardModel ToEntity(this HeuteBoard board)
    {
        return new BoardModel
        {
            Id = board.Id,
            OwnerId = board.OwnerId,
            LayoutId = board.LayoutId,
            Date = board.Date,
            Cards = [.. board.Cards.Select(c => c.ToEntity(board.Id))]
        };
    }
}