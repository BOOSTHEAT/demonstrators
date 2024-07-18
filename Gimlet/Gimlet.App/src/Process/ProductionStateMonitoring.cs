using Gimlet.Model;
using ImpliciX.Language.Control;
using static ImpliciX.Language.Control.Condition;

namespace Gimlet.App.Process;

public class ProductionStateMonitoring : SubSystemDefinition<ProductionStateMonitoring.State>
{
  public ProductionStateMonitoring()
  {
    var self = pasteurize.monitoring.production_state;
    
    // @formatter:off
    Subsystem(pasteurize.monitoring.production_state)
      .Always
       .Set(self.public_state)           
          .With(ProductionState.Filling)
              .When(Any(InState(self, State.AutomaticFilling),InState(self, State.ForcedFilling)))
          .With(ProductionState.Heating)
              .When(InState(self, State.AutomaticHeating))
          .With(ProductionState.Emptying)
              .When(Any(InState(self, State.AutomaticEmptying),InState(self, State.ForcedEmptying)))
          .With(ProductionState.Stop).Otherwise()
                    
      .Initial(State.Stop)
      
      .Define(State.Stop)
        .Transitions
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Automatic))
            .Then(State.Automatic)
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Manual))
            .Then(State.Forced)
      
      .Define(State.Automatic)
        .Transitions
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Stop))
            .Then(State.Stop)
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Manual))
            .Then(State.Forced)
      
      .Define(State.Forced)
        .Transitions
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Automatic))
            .Then(State.Automatic)
          .When(Is(pasteurize.control_panel.public_state, ControlPublicState.Stop))
            .Then(State.Stop)
      
      .Define(State.AutomaticStop).AsInitialSubStateOf(State.Automatic)
        .Transitions
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Filling))
            .Then(State.AutomaticFilling)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Heating))
            .Then(State.AutomaticHeating)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Emptying))
            .Then(State.AutomaticEmptying)
      
      .Define(State.AutomaticFilling).AsInitialSubStateOf(State.Automatic)
        .Transitions
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Stop))
            .Then(State.AutomaticStop)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Heating))
            .Then(State.AutomaticHeating)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Emptying))
            .Then(State.AutomaticEmptying)
      
      .Define(State.AutomaticHeating).AsInitialSubStateOf(State.Automatic)
        .Transitions
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Filling))
            .Then(State.AutomaticFilling)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Stop))
            .Then(State.AutomaticStop)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Emptying))
            .Then(State.AutomaticEmptying)
      
      .Define(State.AutomaticEmptying).AsInitialSubStateOf(State.Automatic)
        .Transitions
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Filling))
            .Then(State.AutomaticFilling)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Heating))
            .Then(State.AutomaticHeating)
          .When(Is(pasteurize.automatic_process.public_state, ProductionState.Stop))
            .Then(State.AutomaticStop)

            
      .Define(State.ForcedStop).AsInitialSubStateOf(State.Forced)
        .Transitions
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Filling))
            .Then(State.ForcedFilling)
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Emptying))
            .Then(State.ForcedEmptying)
      
      .Define(State.ForcedFilling).AsInitialSubStateOf(State.Forced)
        .Transitions
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Stop))
            .Then(State.ForcedStop)
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Emptying))
            .Then(State.ForcedEmptying)
      
      .Define(State.ForcedEmptying).AsInitialSubStateOf(State.Forced)
        .Transitions
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Filling))
            .Then(State.ForcedFilling)
          .When(Is(pasteurize.forced_process.public_state, ProductionState.Stop))
            .Then(State.ForcedStop);

}

  public enum State
  {
    Stop,
    Automatic,
    AutomaticStop,
    AutomaticFilling,
    AutomaticHeating,
    AutomaticEmptying,
    Forced,
    ForcedStop,
    ForcedFilling,
    ForcedEmptying,
  }
}