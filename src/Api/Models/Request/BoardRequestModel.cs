namespace HeuteApp.Api.Models.Request;

public record GetBoardRequest(Guid OwnerId, DateOnly Date);