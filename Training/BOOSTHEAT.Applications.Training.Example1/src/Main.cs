using System;
using System.Collections.Generic;
using ImpliciX.Language;
using ImpliciX.Language.Control;
using ImpliciX.Language.Driver;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using ImpliciX.Language.Store;

namespace BOOSTHEAT.Applications.Training.Example1;

public class Main : ApplicationDefinition
{
  public Main()
  {
    AppName = "Training Example 1";
    AppSettingsFile = "appsettings.json";

    DataModelDefinition = new DataModelDefinition
    {
      Assembly = typeof(training).Assembly
    };

    ModuleDefinitions = new object[]
    {
      new ControlModuleDefinition
      {
        Assembly = typeof(PumpControl).Assembly
      },
      
      new UserInterfaceModuleDefinition { UserInterface = Gui.Definition },
      
      new DriverModbusModuleDefinition
      {
        Slaves = new []
        {
          CarteHaut.DefineBoard(training.io_board)
        }
      },
      
      new PersistentStoreModuleDefinition
      {
        DefaultVersionSettings = new Dictionary<Urn, (string Name, string Value)[]>
        {
          { training.running_threshold, new[] { ("value", "320") } },
          { training.stopping_threshold, new[] { ("value", "300") } },
          { training.calculation, new[] { ("a0", "1633"), ("a1", "-11"), ("a2", "0.01855") } },
          { training.thingsboard.delay_before_retry, new[] { ("value", "30") } },
          { training.thingsboard.delay_before_enable, new[] { ("value", "3600") } },
        },
        DefaultUserSettings = new Dictionary<Urn, (string Name, string Value)[]>
        {
          { training.thingsboard.host, new[] { ("value", "thingsboard.cloud") } },
          { training.thingsboard.access_token, new[] { ("value", "XXXXXXXX") } },
        },
      },
      
      new ThingsBoardModuleDefinition
      {
        RetryDelay = training.thingsboard.delay_before_retry,
        EnableDelay = training.thingsboard.delay_before_enable,
        Connection =
        {
          Host = training.thingsboard.host,
          AccessToken = training.thingsboard.access_token
        },
        Telemetry = new (Urn, TimeSpan)[]
        {
          (training.return_temperature.measure, TimeSpan.FromSeconds(10)),
          (training.pump_power_consumption.measure, TimeSpan.FromSeconds(10)),
          (training.pump_throttle.measure, TimeSpan.FromSeconds(10))
        }
      },
      
      new DriverSimulationModuleDefinition
      {
        Properties = sim => new[]
        {
          sim.Sinusoid(training.return_temperature, 280, 370, 0.95),
          sim.Sinusoid(training.pump_power_consumption, 10, 10000, 0.95),
        }
      }
    };
  }
}