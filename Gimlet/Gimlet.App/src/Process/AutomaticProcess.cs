using Gimlet.Model;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace Gimlet.App.Process;

public class AutomaticProcess:SubSystemDefinition<AutomaticProcess.State>
{
    public AutomaticProcess()
    {
        var self = pasteurize.automatic_process;
    // @formatter:off
    Subsystem(pasteurize.automatic_process)
        .Always
         .Set(self.public_state)           
            .With(ProductionState.Filling)
                .When(InState(self, State.Filling))
            .With(ProductionState.Heating)
                .When(InState(self, State.Heating))
            .With(ProductionState.Emptying)
                .When(Any(InState(self, State.Emptying),InState(self, State.Finalizing)))
            .With(ProductionState.Stop).Otherwise()
                    
        .Initial(State.Stopped)
       
        .Define(State.Stopped)
            .OnEntry
                .Set(pasteurize.actuators.brewer._supply, PowerSupply.Off)
                .Set(pasteurize.actuators.heater._supply, PowerSupply.Off)
                .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Close)
                .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Close)
                .Set(pasteurize.run, Presence.Disabled)
            .Transitions
                .WhenMessage(pasteurize.automatic_process._jump, NameOf(State.Running)).Then(State.Running)
        
        .Define(State.Running)
            .Transitions
                .WhenMessage(pasteurize.automatic_process._jump, NameOf(State.Stopped)).Then(State.Stopped)
                .WhenMessage(pasteurize.automatic_process._jump, NameOf(State.Idle)).Then(State.Idle)
                .When(Is(pasteurize.actuators.filling_valve.status.measure, ValveStatus.Failed)).Then(State.Stopped)
                .When(Is(pasteurize.actuators.emptying_valve.status.measure, ValveStatus.Failed)).Then(State.Stopped)
                .When(Is(pasteurize.actuators.brewer.status.measure,BrewerStatus.Failed )).Then(State.Stopped)
                .When(Is(pasteurize.actuators.heater.status.measure,HeaterStatus.Failed )).Then(State.Stopped)

        .Define(State.Filling).AsInitialSubStateOf(State.Running)
            .OnEntry
                .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Open)
            .Transitions
                .When(GreaterOrEqualTo(pasteurize.tank.liquid_level.measure, pasteurize.tank.max_level)).Then(State.Heating)
        
        .Define(State.Heating).AsSubStateOf(State.Running)
            .OnEntry
                .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Close)
                .Set(pasteurize.actuators.brewer._supply, PowerSupply.On)
                .Set(pasteurize.actuators.heater._supply, PowerSupply.On)
                .StartTimer(pasteurize.actuators.heater.heating_duration)
            .OnExit
                .Set(pasteurize.actuators.heater._supply, PowerSupply.Off)
            .Transitions
                .WhenTimeout(pasteurize.actuators.heater.heating_duration).Then(State.Emptying)
        
        .Define(State.Emptying).AsSubStateOf(State.Running)
            .OnEntry
                .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Open)
            .Transitions
                .When(LowerOrEqualTo(pasteurize.tank.liquid_level.measure, pasteurize.tank.min_level)).Then(State.Finalizing)
        
        .Define(State.Finalizing).AsSubStateOf(State.Running)
            .OnEntry
                .Set(pasteurize.actuators.brewer._supply, PowerSupply.Off)
                .Set(pasteurize.actuators.heater._supply, PowerSupply.Off)
                .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Close)
                .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Close)
            .Transitions
                .When(Is(pasteurize.actuators.filling_valve.status.measure, ValveStatus.Closed)
                                    .And(Is(pasteurize.actuators.emptying_valve.status.measure, ValveStatus.Closed))
                                    .And(Is(pasteurize.actuators.brewer.status.measure, BrewerStatus.Stopped)
                                    .And(Is(pasteurize.actuators.heater.status.measure, HeaterStatus.Stopped))
                                    )).Then(State.Filling)
       .Define(State.Idle).AsSubStateOf(State.Running)
           .Transitions
                .WhenMessage(pasteurize.automatic_process._jump, NameOf(State.Emptying)).Then(State.Emptying)
                .WhenMessage(pasteurize.automatic_process._jump, NameOf(State.Filling)).Then(State.Filling);

    

        
    }
    
    public enum State
    {
        Idle,
        Stopped,
        Running,
        Filling,
        Heating,
        Emptying,
        Finalizing,
    }
}

