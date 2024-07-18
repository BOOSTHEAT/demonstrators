using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace BOOSTHEAT.Applications.Training.Timer;

public class TimerControl : SubSystemDefinition<TimerControl.State>
{
  public TimerControl()
  {
    // @formatter:off
    Subsystem(_.timer_control)
      .Initial(State.Stopped)
      .Define(State.Stopped)
        .Transitions
          .When(Is(_.active, Presence.Enabled))
            .Then(State.Waiting)
      .Define(State.Waiting)
        .OnEntry
          .StartTimer(_.activation_delay)
        .Transitions
          .When(Is(_.active, Presence.Disabled))
            .Then(State.Stopped)
          .WhenTimeout(_.activation_delay)
            .Then(State.Running)
      .Define(State.Running)
        .Transitions
          .When(Is(_.active, Presence.Disabled))
            .Then(State.Stopped);
    // @formatter:on
  }
  
  public enum State
  {
    Stopped,
    Waiting,
    Running,
  }  
}