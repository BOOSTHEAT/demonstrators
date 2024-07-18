using Gimlet.App.Process;
using Gimlet.Model;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;
/*
namespace Gimlet.App
{
    public class Processing : SubSystemDefinition<Processing.State>
    {
        public Processing() 
        {
            Subsystem(system.processing)
              .Initial(State.Stopped)
              .Define(State.Stopped)
                .Transitions
                  .When(Is(system.processing.presence, Presence.Enabled))
                    .Then(State.Running)
              
              .Define(State.Running)
                .OnState
                  .Set(system.metrics.mttr_motor, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.motorstate,MotorFailure.State.Failure.ToString(),"duration") , MetricUrn.Build(system.metrics.motorstate,MotorFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mtbf_motor, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.motorstate,MotorFailure.State.NotFailure.ToString(),"duration") , MetricUrn.Build(system.metrics.motorstate,MotorFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mttr_heater, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.heaterstate,HeaterFailure.State.Failure.ToString(),"duration") , MetricUrn.Build(system.metrics.heaterstate,HeaterFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mtbf_heater, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.heaterstate,HeaterFailure.State.NotFailure.ToString(),"duration") , MetricUrn.Build(system.metrics.heaterstate,HeaterFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mttr_filling, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.fillingstate,FillingFailure.State.Failure.ToString(),"duration") , MetricUrn.Build(system.metrics.fillingstate,FillingFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mtbf_filling, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.fillingstate,FillingFailure.State.NotFailure.ToString(),"duration") , MetricUrn.Build(system.metrics.fillingstate,FillingFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mttr_emptying, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.emptyingstate,EmptyingFailure.State.Failure.ToString(),"duration") , MetricUrn.Build(system.metrics.emptyingstate,EmptyingFailure.State.Failure.ToString(),"occurrence"))
                  .Set(system.metrics.mtbf_emptying, Division.Func , system.processing.division, MetricUrn.Build(system.metrics.emptyingstate,EmptyingFailure.State.NotFailure.ToString(),"duration") , MetricUrn.Build(system.metrics.emptyingstate,EmptyingFailure.State.Failure.ToString(),"occurrence"))
                
                .Transitions
                  .When(Is(system.processing.presence, Presence.Disabled))
                    .Then(State.Stopped);               
        }

        public enum State
        {
            Stopped,
            Running,
            
        }


    }
} 
*/
