using ImpliciX.Language.Model;

namespace Gimlet.PlcSim;

public static class DefaultValues
{
  public static readonly IDictionary<Urn, (string Name, string Value)[]> UserSettings = new Dictionary<Urn, (string Name, string Value)[]>
  {
    {device._.modbusapi_presence, V(nameof(Presence.Enabled))},
    {data.filling_valve.failure, V(nameof(Presence.Disabled))},
    {data.heater.failure, V(nameof(Presence.Disabled))},
    {data.brewer.failure, V(nameof(Presence.Disabled))},
    {data.emptying_valve.failure, V(nameof(Presence.Disabled))},
  };

  public static readonly IDictionary<Urn, (string Name, string Value)[]> VersionSettings = new Dictionary<Urn, (string Name, string Value)[]>
  {
    {data.filling_valve.status, V(nameof(OpenClose.Close))},
    {data.filling_valve.stopped_change, V("0.0")},
    {data.filling_valve.running_change, V("0.002")},
    {data.heater.status, V(nameof(RunStop.Stop))},
    {data.heater.running_change,
      F(("t0", "320.0"), ("velocity", "4.0"), ("inertia", "50.0"), ("ambient", "320.0"))},
    {data.heater.stopped_change,
      F(("t0", "360.0"), ("velocity", "-4.0"), ("inertia", "70.0"), ("ambient", "320.0"))},
    {data.tank.level_compute, F(("level_min", "0.05"), ("level_max", "0.95"))},
    {data.brewer.status, V(nameof(RunStop.Stop))},
    {data.brewer.running_change, V("0")},
    {data.brewer.stopped_change, V("0")},
    {data.emptying_valve.status, V(nameof(OpenClose.Close))},
    {data.emptying_valve.running_change, V("0.002")},
    {data.emptying_valve.stopped_change, V("0.0")},
  };

  private static (string Name, string Value)[] V(string v) => new[] { ("value", v) };
  private static (string Name, string Value)[] F(params (string Name, string Value)[] a) => a;
}

