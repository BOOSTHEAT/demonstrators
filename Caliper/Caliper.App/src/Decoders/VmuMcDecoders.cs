using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Modbus.RegistersConverterHelper;

namespace Caliper.App.Decoders;

internal static class VmuMcDecoders
{
    public static readonly MeasureDecoder VmuMcSoftwareVersion =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<SoftwareVersion>.Create(measureUrn, statusUrn,
            SoftwareVersion.Create(registers[0], 0, 0, 0), currentTime);

    public static readonly MeasureDecoder VmuMcGasIndex =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Volume>.Create(measureUrn, statusUrn,
            Volume.FromFloat((float) ToUnsignedIntMswLast(registers) / 100), currentTime);

    public static readonly MeasureDecoder VmuMcElectricalIndex =
        (measureUrn, statusUrn, registers, currentTime, _) => Measure<Energy>.Create(measureUrn, statusUrn,
            Energy.FromFloat((float) ToUnsignedIntMswLast(registers) * 1), currentTime); //unit Wh
}