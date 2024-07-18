using System;
using System.Collections.Generic;
using Gimlet.Model;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;

namespace Gimlet.App.Modbus;

public class PLC
{
    private static readonly HardwareDeviceNode HwSwDeviceNode = device._.plc;
        

    public static readonly Func<ModbusSlaveDefinition> Definition = () => new(HwSwDeviceNode)
    {
        Name = nameof(PLC),
        SettingsUrns = new Urn[]
        {
            HwSwDeviceNode.presence,
        },
        ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>()
        {
            [MapKind.MainFirmware] =
                // @formatter:off
                RegistersMap.Create()
                    .RegistersSegmentsDefinitions(new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 0, RegistersToRead = 8})
                    .For(pasteurize.tank.liquid_temperature).DecodeRegisters(0, 2, Supplies.TemperatureDecoder)
                    .For(pasteurize.tank.liquid_level).DecodeRegisters(2, 2, Supplies.LevelDecoder)
                    .For(pasteurize.actuators.filling_valve.status).DecodeRegisters(4, 1, Supplies.ValveDecoder)
                    .For(pasteurize.actuators.emptying_valve.status).DecodeRegisters(5, 1, Supplies.ValveDecoder)
                    .For(pasteurize.actuators.brewer.status).DecodeRegisters(6, 1, Supplies.BrewerDecoder)
                    .For(pasteurize.actuators.heater.status).DecodeRegisters(7, 1, Supplies.HeaterDecoder)

            // @formatter:on
        },
        CommandMap = CommandMap.Empty()
            .Add(pasteurize.actuators.filling_valve._switch, (v,_,__) => Command.Create(100,new []{Convert.ToUInt16(v)}))
            .Add(pasteurize.actuators.emptying_valve._switch, (v,_,__) => Command.Create(101,new []{Convert.ToUInt16(v)}))
            .Add(pasteurize.actuators.brewer._supply, (v,_,__) => Command.Create(102,new []{Convert.ToUInt16(v)}))
            .Add(pasteurize.actuators.heater._supply, (v,_,__) => Command.Create(103,new []{Convert.ToUInt16(v)}))
    };
}