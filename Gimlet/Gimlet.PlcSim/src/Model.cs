using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;

namespace Gimlet.PlcSim;

public class device : Device
{
  public static device _ { get; } = new ();

  private device() : base(nameof(device))
  {
    modbusapi_presence = UserSettingUrn<Presence>.Build(Urn, nameof(modbusapi_presence));
  }
  
  public UserSettingUrn<Presence> modbusapi_presence { get; }
}

public class data : RootModelNode
{
  public data() : base(nameof(data))
  {
  }
  
  static data() {
    var self = new data();
    filling_valve = new Pod<OpenClose,OpenClose,Percentage>(nameof(filling_valve), self);
    emptying_valve = new Pod<OpenClose,OpenClose,Percentage>(nameof(emptying_valve), self);
    heater = new Pod<PowerSupply,RunStop,FunctionDefinition>(nameof(heater), self);
    brewer = new Pod<PowerSupply,RunStop,Percentage>(nameof(brewer), self);
    tank = new Tank(nameof(tank), self);
    main_screen = new GuiNode(self, nameof(main_screen));
  }
  public static Pod<OpenClose,OpenClose,Percentage> filling_valve { get; }
  public static Pod<OpenClose,OpenClose,Percentage> emptying_valve { get; }
  public static Pod<PowerSupply,RunStop,FunctionDefinition> heater { get; }
  public static Pod<PowerSupply,RunStop,Percentage> brewer { get; }
  public static Tank tank { get; } 
  public static GuiNode main_screen { get; }

  public class Tank : SubSystemNode
  {
    public Tank(string name, ModelNode parent) : base(name, parent)
    {
      temperature = PropertyUrn<Temperature>.Build(Urn, nameof(temperature));
      level = PropertyUrn<Percentage>.Build(Urn, nameof(level));
      level_compute = VersionSettingUrn<FunctionDefinition>.Build(Urn, nameof(level_compute));
    }
    public PropertyUrn<Temperature> temperature { get; }
    public PropertyUrn<Percentage> level { get; }
    public VersionSettingUrn<FunctionDefinition> level_compute { get; }
  }
}


[ValueObject]
public enum RunStop
{
  Stop = 0,
  Run = 1,
  Failed = 2,
}

[ValueObject]
public enum OpenClose
{
  Close = 0,
  Open = 1,
  Failed = 2,
}