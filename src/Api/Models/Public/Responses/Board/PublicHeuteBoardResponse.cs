namespace HeuteApp.Api.Models.Public.Responses.Board;

public sealed record PublicHeuteBoardResponse(
    DateOnly Date,
    string LayoutName,
    IReadOnlyCollection<PublicBoardCardResponse> Cards);