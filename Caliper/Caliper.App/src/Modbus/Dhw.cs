using System;
using System.Collections.Generic;
using Caliper.Model;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static Caliper.App.Decoders.PollustatDecoders;

namespace Caliper.App.Modbus;

public static class DhwPollustat
{
    private static readonly HardwareAndSoftwareDeviceNode DeviceNode = monitoring.dhw;
    
    public static readonly Func<ModbusSlaveDefinition> Definition = () => new ModbusSlaveDefinition(DeviceNode)
    {
        Name = nameof(DhwPollustat),
        SettingsUrns = new Urn[] { DeviceNode.presence },
        ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>()
        {
            [MapKind.MainFirmware] =
          // @formatter:off
          RegistersMap.Create()
          .RegistersSegmentsDefinitions(
              new RegistersSegmentsDefinition(RegisterKind.Input){StartAddress = 0, RegistersToRead = 75},
              new RegistersSegmentsDefinition(RegisterKind.Input){StartAddress = 2004, RegistersToRead = 1}
          )
          .For(monitoring.dhw.energy).DecodeRegisters(5, 2, PollustatHeatingEnergy)
          .For(monitoring.dhw.power).DecodeRegisters(54, 2, PollustatHeatOutput)
          .For(monitoring.dhw.flow).DecodeRegisters(61, 2, PollustatHeatingFlow)
          .For(monitoring.dhw.outlet_temperature).DecodeRegisters(65, 2, PollustatSupplyTemperature)
          .For(monitoring.dhw.inlet_temperature).DecodeRegisters(69, 2, PollustatReturnTemperature)
          .For(monitoring.dhw.differential_temperature).DecodeRegisters(73, 2, PollustatHeatingDifferentialTemperature)
          .For(DeviceNode.software_version).DecodeRegisters(2004, 1, PollustatSoftwareVersion)
            // @formatter:on
        }
    };
}