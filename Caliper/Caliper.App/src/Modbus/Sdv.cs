using System;
using System.Collections.Generic;
using Caliper.Model;
using Caliper.Model.Tree;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static Caliper.App.Decoders.SdvDecoders;

namespace Caliper.App.Modbus;

internal static class Sdv
{
    private static readonly HardwareAndSoftwareDeviceNode DeviceNode = monitoring.product;

    public static readonly Func<ModbusSlaveDefinition> Definition = () => new (DeviceNode)
    {
        Name=nameof(Sdv),
        SettingsUrns = new Urn[] { DeviceNode.presence },
        ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>
        {
            [MapKind.MainFirmware] =
            // @formatter:off    
            RegistersMap.Create()
            .RegistersSegmentsDefinitions(
                new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 40000,RegistersToRead = 38},
                new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 45000,RegistersToRead = 10}
            )
            .For(monitoring.product.outdoor_temperature).DecodeRegisters(40032, 2, TemperatureDecoder)
            .For(monitoring.product.room_temperature).DecodeRegisters(40020, 2, TemperatureDecoder)
            .For(monitoring.product.compressor_running_time).DecodeRegisters(40034, 2, DurationDecoder)
            .For(monitoring.product.dhw_state).DecodeRegisters(45003, 1, StateDecoder)
            .For(monitoring.product.heating_service_state).DecodeRegisters(45004, 1, StateDecoder)
            .For(monitoring.product.heat_pump_state).DecodeRegisters(45005, 1, StateDecoder)
            .For(monitoring.product.condensing_boiler_state).DecodeRegisters(45006, 1, StateDecoder)
            // @formatter:on
        }
    };
}