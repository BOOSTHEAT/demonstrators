using Gimlet.Model;
using ImpliciX.Language.Control;

namespace Gimlet.App.Process;

public class FillingValveHealthMonitoring: SubSystemDefinition<FillingValveHealthMonitoring.State>
{
    public FillingValveHealthMonitoring()
    {
        // @formatter:off
        Subsystem(pasteurize.actuators.filling_valve.health)
            .Initial(State.Nominal)
            .Define(State.Nominal).Transitions.When(Condition.Is(pasteurize.actuators.filling_valve.status.measure, ValveStatus.Failed))
                .Then(State.Failure)
            .Define(State.Failure).Transitions.When(Condition.IsNot(pasteurize.actuators.filling_valve.status.measure, ValveStatus.Failed))
                .Then(State.Nominal);
    }
    public enum State
    {
        Nominal,
        Failure
    }
    
}

public class EmptyingValveHealthMonitoring: SubSystemDefinition<EmptyingValveHealthMonitoring.State>
{
    public EmptyingValveHealthMonitoring()
    {
        // @formatter:off
        Subsystem(pasteurize.actuators.emptying_valve.health)
            .Initial(State.Nominal)
            .Define(State.Nominal).Transitions
                .When(Condition.Is(pasteurize.actuators.emptying_valve.status.measure, ValveStatus.Failed))
                .Then(State.Failure)
            .Define(State.Failure).Transitions
                .When(Condition.IsNot(pasteurize.actuators.emptying_valve.status.measure, ValveStatus.Failed))
                .Then(State.Nominal);
    }
    public enum State
    {
        Nominal,
        Failure
    }
}

public class BrewerHealthMonitoring: SubSystemDefinition<BrewerHealthMonitoring.State>
{
    public BrewerHealthMonitoring()
    {
        // @formatter:off
        Subsystem(pasteurize.actuators.brewer.health)
            .Initial(State.Nominal)
            .Define(State.Nominal).Transitions.When(Condition.Is(pasteurize.actuators.brewer.status.measure, BrewerStatus.Failed))
                .Then(State.Failure)
            .Define(State.Failure).Transitions.When(Condition.IsNot(pasteurize.actuators.brewer.status.measure, BrewerStatus.Failed))
                .Then(State.Nominal);
    }
    public enum State
    {
        Nominal,
        Failure
    }
}

public class HeaterHealthMonitoring: SubSystemDefinition<HeaterHealthMonitoring.State>
{
    public HeaterHealthMonitoring()
    {
        // @formatter:off
        Subsystem(pasteurize.actuators.heater.health)
            .Initial(State.Nominal)
            .Define(State.Nominal).Transitions.When(Condition.Is(pasteurize.actuators.heater.status.measure, HeaterStatus.Failed))
                .Then(State.Failure)
            .Define(State.Failure).Transitions.When(Condition.IsNot(pasteurize.actuators.heater.status.measure, HeaterStatus.Failed))
                .Then(State.Nominal);
    }
    public enum State
    {
        Nominal,
        Failure
    }
}