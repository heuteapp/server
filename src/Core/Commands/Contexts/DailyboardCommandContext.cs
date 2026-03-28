using HeuteApp.Core.Aggregates.Dailyboard;
using HeuteApp.Core.Aggregates.Layout;

namespace HeuteApp.Core.Commands.Contexts;

public record DailyboardCommandContext(
    HeuteDailyboard Dailyboard,
    HeuteLayout Layout
);