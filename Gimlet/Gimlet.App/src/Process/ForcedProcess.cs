using Gimlet.Model;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace Gimlet.App.Process;

public class ForcedProcess : SubSystemDefinition<ForcedProcess.State>
{
    public ForcedProcess()
    {
        var self = pasteurize.forced_process;

        // @formatter:off
        Subsystem(pasteurize.forced_process)
            .Always
             .Set(self.public_state)           
                .With(ProductionState.Filling)
                    .When(InState(self, State.Filling))
                .With(ProductionState.Emptying)
                    .When(InState(self, State.Emptying))
                .With(ProductionState.Stop).Otherwise()
            
            .Initial(State.Stopped)
            .Define(State.Stopped)
                .Transitions
                    .WhenMessage(pasteurize.forced_process._jump, NameOf(State.Filling)).Then(State.Filling)
                    .WhenMessage(pasteurize.forced_process._jump, NameOf(State.Emptying)).Then(State.Emptying)
            
            .Define(State.Filling)
                .OnEntry
                    .Set(pasteurize.actuators.brewer._supply, PowerSupply.Off)
                    .Set(pasteurize.actuators.heater._supply, PowerSupply.Off)
                    .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Close)
                    .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Open)
                .OnExit
                    .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Close)
                .Transitions
                   .When(GreaterOrEqualTo(pasteurize.tank.liquid_level.measure, pasteurize.tank.max_level)).Then(State.Stopped)
                   .WhenMessage(pasteurize.forced_process._jump, NameOf(State.Emptying)).Then(State.Emptying)
            
            .Define(State.Emptying)
                .OnEntry
                    .Set(pasteurize.actuators.brewer._supply, PowerSupply.Off)
                    .Set(pasteurize.actuators.heater._supply, PowerSupply.Off)
                    .Set(pasteurize.actuators.filling_valve._switch, ValvePosition.Close)
                    .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Open)
                .OnExit
                    .Set(pasteurize.actuators.emptying_valve._switch, ValvePosition.Close)
                .Transitions
                     .When(LowerOrEqualTo(pasteurize.tank.liquid_level.measure, pasteurize.tank.min_level)).Then(State.Stopped)
                     .WhenMessage(pasteurize.forced_process._jump, NameOf(State.Filling)).Then(State.Filling);
            
    }
    
    
    public enum State
    {
        Filling,
        Emptying,
        Stopped,
    }
}