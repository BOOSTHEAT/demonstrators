using ImpliciX.Language.Model;

namespace Gimlet.Model;

public class monitoring : ModelNode
{
  public monitoring(ModelNode parent) : base(nameof(monitoring), parent)
  {
    tank_liquid_temperature = MetricUrn.Build(Urn, nameof(tank_liquid_temperature));
    tank_liquid_level = MetricUrn.Build(Urn, nameof(tank_liquid_level));

    production_state = new production_state(this);

    //fake data for kpis

    kpi_brewer_failed_duration = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_brewer_failed_duration));
    kpi_brewer_nominal_duration = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_brewer_nominal_duration));
    kpi_brewer_faults_count = UserSettingUrn<Counter>.Build(Urn, nameof(kpi_brewer_faults_count));

    kpi_heater_failed_duration = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_heater_failed_duration));
    kpi_heater_nominal_duration = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_heater_nominal_duration));
    kpi_heater_faults_count = UserSettingUrn<Counter>.Build(Urn, nameof(kpi_heater_faults_count));


    kpi_heater_mtbf = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_heater_mtbf));
    kpi_heater_mttr = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_heater_mttr));

    kpi_brewer_mtbf = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_brewer_mtbf));
    kpi_brewer_mttr = UserSettingUrn<Duration>.Build(Urn, nameof(kpi_brewer_mttr));

    // end fake data for kpis
  }

  public MetricUrn tank_liquid_temperature { get; }
  public MetricUrn tank_liquid_level { get; }
  public production_state production_state { get; }

  public UserSettingUrn<Duration> kpi_brewer_mttr { get; set; }

  public UserSettingUrn<Duration> kpi_brewer_mtbf { get; set; }

  public UserSettingUrn<Duration> kpi_heater_mttr { get; set; }

  public UserSettingUrn<Duration> kpi_heater_mtbf { get; set; }

  public UserSettingUrn<Counter> kpi_heater_faults_count { get; set; }

  public UserSettingUrn<Duration> kpi_heater_nominal_duration { get; set; }

  public UserSettingUrn<Duration> kpi_heater_failed_duration { get; set; }

  public UserSettingUrn<Counter> kpi_brewer_faults_count { get; set; }

  public UserSettingUrn<Duration> kpi_brewer_nominal_duration { get; set; }

  public UserSettingUrn<Duration> kpi_brewer_failed_duration { get; set; }
}

public class production_state : SubSystemNode
{
  public production_state(ModelNode parent) : base(nameof(production_state), parent)
  {
    public_state = PropertyUrn<ProductionState>.Build(Urn, nameof(public_state));
    overview = MetricUrn.Build(Urn, nameof(overview));

  }
  public PropertyUrn<ProductionState> public_state { get; }
  public MetricUrn overview { get; }

}