using System.Collections.Generic;
using System.Drawing;
using Gimlet.Model;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Gimlet.App.GUI;

public class StatePie : Screens
{
  public static IEnumerable<AlignedBlock> Screen()
  {
    yield return At.Right(0).Top(0)
      .Put(Image("Assets/Background_pie.png"));
    
    yield return At.Right(170).Top(60).Put(
      Chart.Pie(
        Of(MetricUrn.BuildDuration(pasteurize.monitoring.production_state.overview, ProductionState.Filling.ToString())).Fill(Color.Red),
        Of(MetricUrn.BuildDuration(pasteurize.monitoring.production_state.overview, ProductionState.Heating.ToString())).Fill(Color.Yellow),
        Of(MetricUrn.BuildDuration(pasteurize.monitoring.production_state.overview, ProductionState.Emptying.ToString())).Fill(Color.Green),
        Of(MetricUrn.BuildDuration(pasteurize.monitoring.production_state.overview, ProductionState.Stop.ToString())).Fill(Color.Blue)
      ).Width(440).Height(440));
  }
}