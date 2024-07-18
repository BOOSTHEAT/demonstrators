using System.Collections.Generic;
using ImpliciX.Language.Model;

namespace Gimlet.Model;

public static class DefaultValues
{
  private const string Disabled = "Disabled";
  private const string Enabled = "Enabled";
  public static readonly IDictionary<Urn, (string Name, string Value)[]> UserSettings =
    new Dictionary<Urn, (string Name, string Value)[]>
    {
      {device._.locale, V(nameof(Locale.fr_FR))},
      {device._.timezone, V(nameof(TimeZone.Europe__Paris))},
      {pasteurize.control_panel.mode, V("Automatic")},
      {pasteurize.tank.min_level, V("0.2")},
      {pasteurize.tank.max_level, V("0.8")},
      {device._.plc.presence, V(Enabled)},
      
      
      //fake data for kpis
      
      {pasteurize.monitoring.kpi_brewer_nominal_duration, V("3000") },
      {pasteurize.monitoring.kpi_brewer_failed_duration, V("600") },
      {pasteurize.monitoring.kpi_brewer_faults_count, V("4") },
      {pasteurize.monitoring.kpi_brewer_mtbf, V("750") },
      {pasteurize.monitoring.kpi_brewer_mttr, V("150") },
      
      {pasteurize.monitoring.kpi_heater_nominal_duration, V("3550") },
      {pasteurize.monitoring.kpi_heater_failed_duration, V("50") },
      {pasteurize.monitoring.kpi_heater_faults_count, V("1") },
      {pasteurize.monitoring.kpi_heater_mtbf, V("3550") },
      {pasteurize.monitoring.kpi_heater_mttr, V("50") },
      
      
    };

  public static readonly IDictionary<Urn, (string Name, string Value)[]> VersionSettings =
    new Dictionary<Urn, (string Name, string Value)[]>
    {
      {pasteurize.actuators.heater.heating_duration, V("60")},
    };

  private static (string Name, string Value)[] V(string v) => new[] {("value", v)};
  private static (string Name, string Value)[] F(params (string Name, string Value)[] a) => a;
   
}