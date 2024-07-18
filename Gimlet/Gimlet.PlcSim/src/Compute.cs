using ImpliciX.Language.Control;
using ImpliciX.Language.Core;
using ImpliciX.Language.Model;

namespace Gimlet.PlcSim;

public class Compute
{
  public static FuncRef Level =>
    new (nameof(Level), () => Create<ComputeLevel>(ComputeLevel.Run), xUrns=>xUrns);
  public static FuncRef Temperature =>
    new (nameof(Temperature), () => Create<ComputeTemperature>(ComputeTemperature.Run), xUrns=>xUrns);

  private static FunctionRun Create<T>(Func<T,FunctionDefinition,(float value, TimeSpan at)[],float> f) where T : new()
  {
    var runner = new T();
    return (functionDefinition, inputs) => f(runner, functionDefinition, inputs);
  }

  class ComputeLevel
  {
    public float CurrentLevel = 0.0f;
    public static float Run(ComputeLevel data, FunctionDefinition functionDefinition, (float value, TimeSpan at)[] inputs)
    {
      var incoming = inputs[0].value;
      var outgoing = inputs[1].value;
      data.CurrentLevel = data.CurrentLevel + incoming - outgoing;
      float level_min = functionDefinition.GetValueParam(nameof(level_min));
      if (data.CurrentLevel < level_min)
        data.CurrentLevel = level_min;
      float level_max = functionDefinition.GetValueParam(nameof(level_max));
      if (data.CurrentLevel > level_max)
        data.CurrentLevel = level_max;
      Log.Debug("Compute tank level: {level}", data.CurrentLevel);
      return data.CurrentLevel;
    }
  }

  class ComputeTemperature
  {
    public float CurrentTemperature = 0.0f;
    public static float Run(ComputeTemperature data, FunctionDefinition functionDefinition, (float value, TimeSpan at)[] inputs)
    {
      float t0 = functionDefinition.GetValueParam(nameof(t0));
      var reference = data.CurrentTemperature / t0;
      float velocity = functionDefinition.GetValueParam(nameof(velocity));
      var direction = 1.0f;
      if (velocity < 0.0f)
      {
        reference = 2.0f - reference;
        direction = -direction;
        velocity = -velocity;
      }
      float inertia = functionDefinition.GetValueParam(nameof(inertia));
      var change = direction * velocity / Math.Pow(reference, inertia);
      data.CurrentTemperature += (float)change;
      float ambient = functionDefinition.GetValueParam(nameof(ambient));
      var level = inputs[0].value;
      var outgoing = inputs[2].value;
      var remaining = level - outgoing;
      var incoming = inputs[1].value;
      data.CurrentTemperature = (ambient * incoming + data.CurrentTemperature * remaining) / (incoming + remaining);
      if (data.CurrentTemperature < ambient)
        data.CurrentTemperature = ambient;
      Log.Debug("Compute tank temperature: {temperature}", data.CurrentTemperature);
      return data.CurrentTemperature;
    }
  }
}