using System;
using System.Reflection;
using Caliper.App.GUI;
using Caliper.App.Modbus;
using Caliper.Model;
using Caliper.Model.Enums;
using ImpliciX.Language;
using ImpliciX.Language.Control;
using ImpliciX.Language.Driver;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;
using ImpliciX.Language.Store;
using Model;


namespace Caliper.App;

public sealed class Main : ApplicationDefinition
{
    public Main()
    {
        AppName = "Caliper";
        AppSettingsFile = "appsettings.json";

        DataModelDefinition = ModuleDefinition.DataModel(device._);

        ModuleDefinitions = new object[]
        {
            new UserInterfaceModuleDefinition { UserInterface = Gui.Definition },

            new ControlModuleDefinition { Assembly = Assembly.GetExecutingAssembly() },
            
            new PersistentStoreModuleDefinition
            {
                DefaultUserSettings = DefaultValues.UserSettings,
                DefaultVersionSettings = DefaultValues.VersionSettings,
                CleanVersionSettings = device._._clean_version_settings
            },

            new DriverSimulationModuleDefinition
            {
                Properties = sim => new[]
                {
                    sim.Stepper(monitoring.pulse_counter.gas_index.measure, 100, TimeSpan.FromSeconds(1), 0.01),
                    sim.Discrete(monitoring.pulse_counter.gas_index.status, MeasureStatus.Success),
                    sim.Stepper(monitoring.pulse_counter.electrical_index.measure, 0, TimeSpan.FromSeconds(1), 400),
                    sim.Discrete(monitoring.pulse_counter.electrical_index.status, MeasureStatus.Success),
                    sim.Sinusoid(monitoring.dhw.power, 0, 100, 1.0),
                    sim.Sinusoid(monitoring.heating.power, 0, 100, 1.0),
                    sim.Sinusoid(monitoring.dhw.energy, 0, 100, 1.0),
                    sim.Sinusoid(monitoring.heating.energy, 0, 100, 1.0),
                    sim.Discrete(monitoring.product.heating_service_state.measure, States.Running, (States.Running, 0.4), (States.Disabled, 0.3),
                        (States.Failure, 0.3)),
                    sim.Sinusoid(monitoring.product.compressor_running_time, 0, 100, 1.0)
                }
            },

            // monitoring:product:heat_pump_state:public_state

            new DriverModbusModuleDefinition
            {
                ModbusSlavesManagement = new ModbusSlaveModel //TODO : Utile ?
                {
                    Commit = device._.software._commit_update,
                    Rollback = device._.software._rollback_update
                },
                Slaves = new []
                {
                    PulseCounter.Definition,
                    HeatingPollustat.Definition,
                    DhwPollustat.Definition,
                    Sdv.Definition
                }
            },

            new MetricsModuleDefinition
            {
                Metrics = AllMetrics.Declarations,
                SnapshotInterval = AllMetrics.SnapshotInterval,
            },

            new TimeCapsuleDefinition
            {
                Metrics = AllMetrics.Declarations,
                UserInterface = Gui.Definition
            },
            
            new HttpTimeSeriesDefinition
            {
                Metrics = AllMetrics.Declarations,
            },
            
            new FrozenTimeSeriesDefinition
            {
                Metrics = AllMetrics.Declarations
            },

            ModuleDefinition.SystemSoftware(device._, s => s == AppName),
            ModuleDefinition.MmiHost(device._),
        };
    }
}