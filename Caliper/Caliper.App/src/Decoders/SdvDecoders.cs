using Caliper.Model.Enums;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using ImpliciX.Language.Modbus;

namespace Caliper.App.Decoders;

internal static class SdvDecoders
{
    public static readonly MeasureDecoder TemperatureDecoder =
        (measureUrn, statusUrn, registers, currentTime, _) =>
        {
            var result = Temperature.FromFloat(RegistersConverterHelper.ToFloatMswLast(registers));
            return Measure<Temperature>.Create(measureUrn, statusUrn, result, currentTime);
        };
    
    public static readonly MeasureDecoder DurationDecoder =
        (measureUrn, statusUrn, registers, currentTime, _) =>
        {
            var duration = RegistersConverterHelper.ToFloatMswLast(registers);
            var result = Duration.FromFloat(duration);
            return Measure<Duration>.Create(measureUrn, statusUrn, result, currentTime);
        };
    
    public static readonly MeasureDecoder StateDecoder =
        (measureUrn, statusUrn, registers, currentTime, _) =>
        {
            var state = RegistersConverterHelper.ToShort(registers);
            var result = (States)state;
            return Measure<States>.Create(measureUrn, statusUrn, result, currentTime);
        };
}