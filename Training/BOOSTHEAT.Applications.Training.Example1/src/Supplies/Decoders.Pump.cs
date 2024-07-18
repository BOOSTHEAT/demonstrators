using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;

namespace BOOSTHEAT.Applications.Training.Example1.Supplies;

public partial class Decoders
{
  public enum PwmKind
  {
    DIRECT,
    REVERSED
  }
  
  public static MeasureDecoder Pump_Grundfos_UMPL25105130_PowerConsumption(PwmKind kind) =>
    (measureUrn, statusUrn, registers, currentTime, _) =>
      Measure<Power>.Create(measureUrn, statusUrn, ToGrundfos_UMPL25105130_PowerConsumption(kind,registers[0]), currentTime);

  private static Result<Power> ToGrundfos_UMPL25105130_PowerConsumption(PwmKind kind, ushort value) =>
    (kind == PwmKind.DIRECT ? value : 1000-value) switch
    {
      { } pct when pct < 730 => Power.FromFloat(pct * 0.2f),
      _ => new InvalidValueError($"The value {value} is not valid as pump consumption measure")
    };


}