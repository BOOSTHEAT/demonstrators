using ImpliciX.Language.Control;
using ImpliciX.Language.Model;

namespace Gimlet.PlcSim;

public class Simulation : SubSystemDefinition<Simulation.State>
{
  public Simulation()
  {
    // @formatter:off
    Subsystem(data.tank)
      .Initial(State.Compute)
      .Define(State.Compute)
        .OnState
          .SetPeriodical(
            data.tank.level, Compute.Level, data.tank.level_compute,
            data.filling_valve.current_change, data.emptying_valve.current_change )
          .SetPeriodical(
            data.tank.temperature, Compute.Temperature, data.heater.current_change,
            data.tank.level, data.filling_valve.current_change, data.emptying_valve.current_change );
    // @formatter:on
  }
  
  public enum State
  {
    Compute
  }
}

public class Valve : PodStateMachine<OpenClose, OpenClose, Percentage>
{
  public Valve(Pod<OpenClose,OpenClose,Percentage> node)
    :base(
      node,
      OpenClose.Open, OpenClose.Close,
      OpenClose.Open, OpenClose.Close, OpenClose.Failed
    )
  {}
}

public class FillingValve : Valve
{
  public FillingValve() :base(data.filling_valve) {}
}

public class EmptyingValve : Valve
{
  public EmptyingValve() :base(data.emptying_valve) {}
}

public class Heater : PodStateMachine<PowerSupply,RunStop,FunctionDefinition>
{
  public Heater()
    :base(
      data.heater,
      PowerSupply.On, PowerSupply.Off,
      RunStop.Run,RunStop.Stop, RunStop.Failed
    )
  {}
}

public class Brewer : PodStateMachine<PowerSupply,RunStop,Percentage>
{
  public Brewer()
    :base(
      data.brewer,
      PowerSupply.On, PowerSupply.Off,
      RunStop.Run,RunStop.Stop, RunStop.Failed
    )
  {}
}

