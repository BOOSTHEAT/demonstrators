using System;
using System.Collections.Generic;
using Caliper.Model;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static Caliper.App.Decoders.VmuMcDecoders;

namespace Caliper.App.Modbus;

internal static class PulseCounter
{
    private static readonly HardwareAndSoftwareDeviceNode DeviceNode = monitoring.pulse_counter;

    public static readonly Func<ModbusSlaveDefinition> Definition = () => new (DeviceNode)
    {
        Name=nameof(PulseCounter),
        SettingsUrns = new Urn[] { DeviceNode.presence },
        ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>
        {
            [MapKind.MainFirmware] =
            // @formatter:off    
            RegistersMap.Create()
            .RegistersSegmentsDefinitions(
                new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 0,RegistersToRead = 4},
                new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 768,RegistersToRead = 1}
            )
            .For(monitoring.pulse_counter.gas_index)
                .DecodeRegisters(0, 2, VmuMcGasIndex)
            .For(monitoring.pulse_counter.electrical_index)
                .DecodeRegisters(2, 2, VmuMcElectricalIndex)
            .For(DeviceNode.software_version)
                .DecodeRegisters(768, 1, VmuMcSoftwareVersion)
            // @formatter:on
        }
    };
}