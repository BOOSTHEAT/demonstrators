using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Modbus.RegistersConverterHelper;
using static Caliper.App.Converters.TemperatureConverters;

namespace Caliper.App.Decoders;

internal static class PollustatDecoders
{
    public static readonly MeasureDecoder PollustatSoftwareVersion =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<SoftwareVersion>.Create(measureUrn, statusUrn,
            SoftwareVersion.Create(
                (ushort) (registers[0] >> 8),
                (ushort) (registers[0] & 0x00FF),
                0, 0),
            currentTime);

    public static readonly MeasureDecoder PollustatHeatingEnergy =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Energy>.Create(measureUrn, statusUrn,
            Energy.FromFloat(ToFloatMswFirst(registers) * 1), currentTime);

    public static readonly MeasureDecoder PollustatHeatOutput =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Power>.Create(measureUrn, statusUrn,
            Power.FromFloat(ToFloatMswFirst(registers) * 1_000), currentTime);

    public static readonly MeasureDecoder PollustatHeatingFlow =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Flow>.Create(measureUrn, statusUrn,
            Flow.FromFloat(ToFloatMswFirst(registers) / 3_600), currentTime);

    public static readonly MeasureDecoder PollustatSupplyTemperature =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Temperature>.Create(measureUrn, statusUrn,
            Temperature.FromFloat(CelsiusToKelvin(ToFloatMswFirst(registers))), currentTime);

    public static readonly MeasureDecoder PollustatReturnTemperature =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Temperature>.Create(measureUrn, statusUrn,
            Temperature.FromFloat(CelsiusToKelvin(ToFloatMswFirst(registers))), currentTime);

    public static readonly MeasureDecoder PollustatHeatingDifferentialTemperature =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<DifferentialTemperature>.Create(measureUrn,
            statusUrn,
            DifferentialTemperature.FromFloat(ToFloatMswFirst(registers)), currentTime);
}