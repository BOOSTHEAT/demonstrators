using System;
using static ImpliciX.Language.Metrics.Metrics;
using ImpliciX.Language.Metrics;
using Gimlet.Model;

namespace Gimlet.App
{
  internal static class AllMetrics
  {
    public static readonly TimeSpan SnapshotInterval = TimeSpan.FromSeconds(5);

    public static readonly IMetricDefinition[] Declarations =
    {
      Metric(pasteurize.monitoring.tank_liquid_temperature).Is.Every(5).Seconds
        .GaugeOf(pasteurize.tank.liquid_temperature.measure)
        .Over.ThePast(1).Weeks,

      Metric(pasteurize.monitoring.tank_liquid_level).Is.Every(5).Seconds
        .GaugeOf(pasteurize.tank.liquid_level.measure)
        .Over.ThePast(1).Weeks,

      Metric(pasteurize.monitoring.production_state.overview).Is.Minutely.OnAWindowOf(1).Hours
        .StateMonitoringOf(pasteurize.monitoring.production_state.public_state)
        .Over.ThePast(1).Weeks,
    };
  }
}