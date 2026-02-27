using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardMapper
{
    public static HeuteBoard ToDomain(this BoardModel model)
    {
        HeuteBoard board = new(
            model.Id,
            model.OwnerId,
            model.LayoutId,
            model.Date
        );

        foreach (var card in model.Cards)
        {
            board.AddCard(card.Id, card.ToProps());
        }

        return board;
    }

    public static BoardModel ToEntity(this HeuteBoard board)
    {
        return new BoardModel
        {
            Id = board.Id,
            OwnerId = board.OwnerId,
            LayoutId = board.LayoutId,
            Date = board.Date,
            Cards = [.. board.Cards.Select(c => c.ToModel(board.Id))]
        };
    }
}