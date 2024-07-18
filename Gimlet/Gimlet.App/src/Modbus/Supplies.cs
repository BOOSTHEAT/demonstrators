using System;
using Gimlet.Model;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;

namespace Gimlet.App.Modbus;

public static class Supplies
{
    private static readonly Func<ushort[], float> ToFloat = RegistersConverterHelper.ToFloatMswLast;

    private static ValveStatus ToValvePosition(ushort[] registers) =>
        registers[0] switch
        {
            0 => ValveStatus.Closed,
            1 => ValveStatus.Opened,
            2 => ValveStatus.Failed,
            _ => throw new NotSupportedException()
        };

    private static BrewerStatus ToBrewerStatus(ushort[] registers) =>
        registers[0] switch
        {
            0 => BrewerStatus.Stopped,
            1 => BrewerStatus.Running,
            2 => BrewerStatus.Failed,
            _ => throw new NotSupportedException()
        };

    private static HeaterStatus ToHeaterStatus(ushort[] registers) =>
        registers[0] switch
        {
            0 => HeaterStatus.Stopped,
            1 => HeaterStatus.Running,
            2 => HeaterStatus.Failed,
            _ => throw new NotSupportedException()
        };
    public static readonly MeasureDecoder TemperatureDecoder = 
        (measureUrn, statusUrn, registers, currentTime, _) => 
            Measure<Temperature>.Create(measureUrn, statusUrn, Temperature.FromFloat(ToFloat(registers)), currentTime);
        
    public static readonly MeasureDecoder LevelDecoder = 
        (measureUrn, statusUrn, registers, currentTime, _) => 
            Measure<Percentage>.Create(measureUrn, statusUrn, Percentage.FromFloat(ToFloat(registers)), currentTime);
        
    public static readonly MeasureDecoder ValveDecoder = 
        (measureUrn, statusUrn, registers, currentTime, _) => 
            Measure<ValveStatus>.Create(measureUrn, statusUrn, ToValvePosition(registers) , currentTime);

    public static readonly MeasureDecoder BrewerDecoder = 
        (measureUrn, statusUrn, registers, currentTime, _) => 
            Measure<BrewerStatus>.Create(measureUrn, statusUrn, ToBrewerStatus(registers) , currentTime);

    public static readonly MeasureDecoder HeaterDecoder = 
        (measureUrn, statusUrn, registers, currentTime, _) => 
            Measure<HeaterStatus>.Create(measureUrn, statusUrn, ToHeaterStatus(registers) , currentTime);
}