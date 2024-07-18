using Caliper.Model;
using ImpliciX.Language.Control;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliper.App
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
                  .Set(system.metrics.heat.energy, Polynomial2.Func, system.processing.addition, system.metrics.heat.heating_energy, system.metrics.heat.dhw_energy)
                  .Set(system.metrics.heat.power, Polynomial2.Func, system.processing.addition, system.metrics.heat.heating_power, system.metrics.heat.dhw_power)
                  .Set(system.metrics.consumption, Polynomial2.Func, system.processing.addition, system.metrics.gas.gas_index, system.metrics.electrical.electrical_index)
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
