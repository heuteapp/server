using HeuteApp.Core.Aggregates;
using HeuteApp.Infrastructure.Models.Aggregates;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardMapper
{
    public static HeuteBoard ToDomain(this HeuteBoardModel model)
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

    public static HeuteBoardModel ToEntity(this HeuteBoard board)
    {
        return new HeuteBoardModel
        {
            Id = board.Id,
            OwnerId = board.OwnerId,
            LayoutId = board.LayoutId,
            Date = board.Date,
            Cards = [.. board.Cards.Select(c => c.ToModel(board.Id))]
        };
    }

    //

    public static void SyncFromDomain(this HeuteBoardModel model, HeuteBoard board)
    {
        model.LayoutId = board.LayoutId;
        model.Date = board.Date;

        var domainCardIds = board.Cards.Select(c => c.Id).ToHashSet();
        var modelCardIds = model.Cards.Select(c => c.Id).ToHashSet();

        foreach (var card in model.Cards.Where(c => !domainCardIds.Contains(c.Id)).ToList())
        {
            model.Cards.Remove(card);
        }

        foreach (var card in board.Cards)
        {
            var existingCard = model.Cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard != null)
            {
                existingCard.Title = card.Title;
                existingCard.SectionId = card.SectionId;
                existingCard.Position = card.Position;
            }
            else
            {
                model.Cards.Add(card.ToModel(board.Id));
            }
        }
    }
}