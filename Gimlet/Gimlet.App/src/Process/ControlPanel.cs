using Gimlet.Model;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace Gimlet.App.Process;

public class ControlPanel:SubSystemDefinition<ControlPanel.State>
{
    public ControlPanel()
    {
        var self = pasteurize.control_panel;

        // @formatter:off
        Subsystem(pasteurize.control_panel)
            .Always
             .Set(self.public_state)           
                .With(ControlPublicState.Automatic)
                    .When(InState(self, State.Automatic))
                .With(ControlPublicState.Stop)
                    .When(InState(self, State.Off))
                .With(ControlPublicState.Manual).Otherwise()
            
            .Initial(State.Off)
            .Define(State.Off)
                .OnEntry
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Stopped))
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Stopped))
                .Transitions.When(Is(pasteurize.run, Presence.Enabled)).Then(State.On)
            
            .Define(State.On)
                .Transitions.When(Is(pasteurize.run, Presence.Disabled)).Then(State.Off)
                            
            .Define(State.Automatic).AsInitialSubStateOf(State.On)
                .OnEntry
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Running))
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Stopped))
                .Transitions
                    .When(Is(pasteurize.control_panel.mode, Mode.ForceFilling)).Then(State.ForceFilling)
                    .When(Is(pasteurize.control_panel.mode, Mode.ForceEmptying)).Then(State.ForceEmptying)
           
            .Define(State.ForceFilling).AsSubStateOf(State.On)
                .OnEntry
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Idle))
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Filling))
                .Transitions
                    .When(Is(pasteurize.control_panel.mode, Mode.Automatic).And(InState(pasteurize.forced_process, ForcedProcess.State.Stopped))).Then(State.Automatic)
                    .When(Is(pasteurize.control_panel.mode, Mode.Automatic).And(InState(pasteurize.forced_process, ForcedProcess.State.Filling))).Then(State.SwitchToAutomaticFromForceFilling)
                    .When(Is(pasteurize.control_panel.mode, Mode.ForceEmptying)).Then(State.ForceEmptying)
            .Define(State.ForceEmptying).AsSubStateOf(State.On)
                .OnEntry
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Idle))
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Emptying))
                .Transitions
                    .When(Is(pasteurize.control_panel.mode, Mode.Automatic).And(InState(pasteurize.forced_process, ForcedProcess.State.Stopped))).Then(State.Automatic)
                    .When(Is(pasteurize.control_panel.mode, Mode.Automatic).And(InState(pasteurize.forced_process, ForcedProcess.State.Emptying))).Then(State.SwitchToAutomaticFromForceEmptying)
                    .When(Is(pasteurize.control_panel.mode, Mode.ForceFilling)).Then(State.ForceFilling)
            
            .Define(State.SwitchToAutomaticFromForceEmptying).AsSubStateOf(State.On)
                .OnEntry
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Stopped))
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Emptying))
                .Transitions
                    .When(Is(pasteurize.control_panel.mode,Mode.Automatic).And(InState(pasteurize.automatic_process, AutomaticProcess.State.Finalizing))).Then(State.Automatic)
            
            .Define(State.SwitchToAutomaticFromForceFilling).AsSubStateOf(State.On)
                .OnEntry
                    .Set(pasteurize.forced_process._jump, NameOf(ForcedProcess.State.Stopped))
                    .Set(pasteurize.automatic_process._jump, NameOf(AutomaticProcess.State.Filling))
                .Transitions
                    .When(Is(pasteurize.control_panel.mode,Mode.Automatic).And(InState(pasteurize.automatic_process, AutomaticProcess.State.Finalizing))).Then(State.Automatic);
    }
    public enum State
    {
        Off,
        On,
        Automatic,
        ForceFilling,
        ForceEmptying,
        SwitchToAutomaticFromForceFilling,
        SwitchToAutomaticFromForceEmptying
    }
}