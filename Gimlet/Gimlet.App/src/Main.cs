using System;
using System.Collections.Generic;
using System.Reflection;
using Gimlet.App.GUI;
using Gimlet.App.Modbus;
using Gimlet.Model;
using ImpliciX.Language;
using ImpliciX.Language.Control;
using ImpliciX.Language.Driver;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Metrics;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Records;
using ImpliciX.Language.StdLib;
using ImpliciX.Language.Store;

namespace Gimlet.App;

public sealed class Main : ApplicationDefinition
{
    public Main()
    {
        AppName = "Gimlet";
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

            new DriverModbusModuleDefinition
            {
                ModbusSlavesManagement = new ModbusSlaveModel
                    { Commit = device._.software._commit_update, Rollback = device._.software._rollback_update },
                Slaves = new []
                {
                    PLC.Definition,
                }
            },
            
            new DriverSimulationModuleDefinition
            {
                Properties = sim => new[]
                {
                    sim.Sinusoid(pasteurize.tank.liquid_temperature, 300, 380, 1.0),
                    sim.Sinusoid(pasteurize.tank.liquid_level, 0.2, 0.8, 1.0),
                    sim.Discrete(pasteurize.actuators.filling_valve.status.measure, ValveStatus.Opened,
                        (ValveStatus.Closed, 0.3), (ValveStatus.Failed, 0.1)),
                    sim.Discrete(pasteurize.actuators.emptying_valve.status.measure, ValveStatus.Opened,
                        (ValveStatus.Closed, 0.3), (ValveStatus.Failed, 0.1)),
                    sim.Discrete(pasteurize.actuators.brewer.status.measure, BrewerStatus.Running,
                        (BrewerStatus.Stopped, 0.3), (BrewerStatus.Failed, 0.1)),
                    sim.Discrete(pasteurize.actuators.heater.status.measure, HeaterStatus.Running,
                        (HeaterStatus.Stopped, 0.3), (HeaterStatus.Failed, 0.1)),
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

            new RecordsModuleDefinition
            {
                Records = AllRecords.Records
            },
            
            ModuleDefinition.SystemSoftware(device._, s => s == AppName),
            ModuleDefinition.MmiHost(device._),
        };
    }
}