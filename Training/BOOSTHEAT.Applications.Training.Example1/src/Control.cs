using BOOSTHEAT.Applications.Training.Example1.Supplies;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace BOOSTHEAT.Applications.Training.Example1;

public class PumpControl : SubSystemDefinition<PumpControl.State>
{
  public PumpControl()
  {
    // @formatter:off
    Subsystem(training.pump_control)
      .Initial(State.Stopped)
      .Define(State.Stopped)
        .OnEntry
          .Set(training.pump_power, PowerSupply.Off)
        .Transitions
          .When(GreaterThan(training.return_temperature.measure, training.running_threshold))
            .Then(State.Running)
      .Define(State.Running)
        .OnEntry
          .Set(training.pump_power, PowerSupply.On)
        .OnState
          .Set(training.pump_throttle, Polynomial1.Func, training.calculation, training.return_temperature.measure)
        .Transitions
          .When(LowerThan(training.return_temperature.measure, training.stopping_threshold))
            .Then(State.Stopped);
    // @formatter:on
  }
  
  public enum State
  {
    Stopped,
    Running,
  }
}