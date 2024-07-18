using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;

namespace BOOSTHEAT.Applications.Training.Example1.Supplies;

public partial class Decoders
{
  public static readonly MeasureDecoder TE_10K3D149K_Temperature =
    ProbeDecode(TE_10K3D149K.Temperature, Temperature.FromFloat);
  
  public static Thermistor TE_10K3D149K =>
    new Thermistor(0.00111777f, 0.00023613f, 0.0000000786f, 340f, 336000f);


  public static MeasureDecoder ProbeDecode<T>(Func<float, Result<float>> converter,
    Func<float, Result<T>> valueObjectFactory) =>
    (measureUrn, statusUrn, registers, currentTime, _) =>
    {
      var modelObject =
        from rawValue in converter(RegistersConverterHelper.ToFloatMswLast(registers))
        from vo in valueObjectFactory(rawValue)
        select vo;
      return Measure<T>.Create(measureUrn, statusUrn, modelObject, currentTime);
    };

  public struct Thermistor
  {
    public Thermistor(float coefA, float coefB, float coefC, float resistanceMin, float resistanceMax)
    {
      CoefA = coefA;
      CoefB = coefB;
      CoefC = coefC;
      ResistanceMin = resistanceMin;
      ResistanceMax = resistanceMax;
    }

    public float CoefA { get; }
    public float CoefB { get; }
    public float CoefC { get; }
    public float ResistanceMin { get; }
    public float ResistanceMax { get; }

    public Result<float> Temperature(float resistance)
    {
      if (resistance < this.ResistanceMin || resistance > this.ResistanceMax) return new ProbeError();
      var logR = MathF.Log(resistance);
      var logR3 = MathF.Pow(logR, 3);
      var temp = 1f / (CoefA + CoefB * logR + CoefC * logR3);
      return MathF.Round(temp, 1);
    }
  }
}