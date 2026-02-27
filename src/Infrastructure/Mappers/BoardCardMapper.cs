using HeuteApp.Core.Entities;
using HeuteApp.Core.Mappers;
using HeuteApp.Infrastructure.Models.Entities;

namespace HeuteApp.Infrastructure.Mappers;

public static class BoardCardMapper
{
    public static BoardCard ToDomain(this BoardCardModel model)
    {
        return model.ToProps().ToDomain(model.Id);
    }

    public static BoardCardProps ToProps(this BoardCardModel model)
    {
        return new BoardCardProps(
            model.Title,
            model.SectionId,
            model.Position
        );
    }

    public static BoardCardModel ToModel(this BoardCard card, Guid boardId)
    {
        return new BoardCardModel()
        {
            BoardId = boardId,
            Title = card.Title,
            SectionId = card.SectionId,
            Position = card.Position
        };
    }
}