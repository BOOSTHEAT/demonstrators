using Gimlet.Model;
using ImpliciX.Language.Records;
using static ImpliciX.Language.Records.Records;

namespace Gimlet.App;

internal static class AllRecords
{
  public static readonly IRecord[] Records =
  {
    Record(general.production_report).Is
      .Last(5).Snapshot
      .Of(general.production_report_input)
      .Instance
  };
}