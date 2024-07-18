using System;
using System.Collections.Generic;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using BOOSTHEAT.Applications.Training.Example1.Supplies;

namespace BOOSTHEAT.Applications.Training.Example1;

  public static class CarteHaut
  { 
    public static Func<ModbusSlaveDefinition> DefineBoard(HardwareAndSoftwareDeviceNode node) => () => new ModbusSlaveDefinition(node, SlaveKind.BH)
    {
      Name = nameof(CarteHaut),
      SettingsUrns = new Urn[]{ node.presence },
      ReadPropertiesMaps = new Dictionary<MapKind, IRegistersMap>()
      {
        [MapKind.MainFirmware] =
        // @formatter:off
        RegistersMap.Create()
        .RegistersSegmentsDefinitions(new RegistersSegmentsDefinition(RegisterKind.Holding){StartAddress = 26,RegistersToRead = 26})    
        .For(training.return_temperature)
            .DecodeRegisters(26, 2, Decoders.TE_10K3D149K_Temperature)
        .For(training.pump_power_consumption)
            .DecodeRegisters(50, 1, Decoders.Pump_Grundfos_UMPL25105130_PowerConsumption(Decoders.PwmKind.DIRECT))
        // @formatter:on
      },
      CommandMap = CommandMap.Empty()
        .Add(
          new GrundfosPump(109,
            110,
            Urn.BuildUrn(nameof(training))),
          training.pump_power,
          training.pump_throttle),
    };
  }