using ImpliciX.Language.Model;

namespace Gimlet.Model;

public class actuators: ModelNode
{
  public actuators(ModelNode parent) : base(nameof(actuators), parent)
  {
    heater = new heater(this);
    brewer = new brewer(this);
    emptying_valve = new valve(this, nameof(emptying_valve));
    filling_valve = new valve(this, nameof(filling_valve));

  }

  public heater heater { get; }
  public brewer brewer { get; }
  public valve emptying_valve { get; }
  public valve filling_valve { get; }

}

public class Actuator : ModelNode
{
  public Actuator(string urnToken, ModelNode parent) : base(urnToken, parent)
  {
    health = new SubSystemNode(nameof(health), this);
  }
  
  public SubSystemNode health { get; }
}

public class brewer : Actuator
{
  public brewer(ModelNode parent) : base(nameof(brewer), parent)
  {
    _supply = CommandNode<PowerSupply>.Create(nameof(_supply), this);
    status = new MeasureNode<BrewerStatus>(nameof(status), this);
  }

  public CommandNode<PowerSupply> _supply { get; }

  public MeasureNode<BrewerStatus> status { get; }
}

public class heater: Actuator
{
  public heater(ModelNode parent) : base(nameof(heater), parent)
  {
    heating_duration = VersionSettingUrn<Duration>.Build(Urn, nameof(heating_duration));
    _supply = CommandNode<PowerSupply>.Create(nameof(_supply), this);
    status = new MeasureNode<HeaterStatus>(nameof(status), this);
  }

  public VersionSettingUrn<Duration> heating_duration { get; }
  public CommandNode<PowerSupply> _supply { get; }
  public MeasureNode<HeaterStatus> status { get; }

}

public class valve : Actuator
{
  public valve(ModelNode parent, string valveName) : base(valveName, parent)
  {
    _switch = CommandNode<ValvePosition>.Create(nameof(_switch), this);
    status = new MeasureNode<ValveStatus>(nameof(status), this);
  }

  public CommandNode<ValvePosition> _switch { get; }
  public MeasureNode<ValveStatus> status { get; }
}


[ValueObject]
public enum HeaterStatus
{
  Stopped = 0,
  Running = 1,
  Failed = 2,
}

[ValueObject]
public enum BrewerStatus
{
  Stopped = 0,
  Running = 1,
  Failed = 2,
}

[ValueObject]
public enum ValveStatus
{
  Closed = 0,
  Opened = 1,
  Failed = 2,
}

[ValueObject]
public enum ValvePosition
{
  Close = 0,
  Open = 1,
}

