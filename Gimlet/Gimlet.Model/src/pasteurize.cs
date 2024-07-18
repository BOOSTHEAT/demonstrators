using ImpliciX.Language.Model;

namespace Gimlet.Model;

public class pasteurize : RootModelNode
{
    public pasteurize() : base(nameof(pasteurize))
    {

    }

    static pasteurize()
    {
        var root = new pasteurize();
        automatic_process = new automatic_process(root);
        forced_process = new forced_process(root);
        control_panel = new control_panel(root);
        mode_selector = new mode_selector(root);
        actuators = new actuators(root);
        tank = new tank(root);
        run = UserSettingUrn<Presence>.Build(root.Urn, nameof(run));
        monitoring = new monitoring(root);
    }

    public static automatic_process automatic_process { get; }
    public static forced_process forced_process { get; }
    public static control_panel control_panel { get; }
    public static mode_selector mode_selector { get; }
    public static actuators actuators { get; }
    public static tank tank { get; }
    public static monitoring monitoring { get; }
    public static UserSettingUrn<Presence> run { get; }
}

public class automatic_process: SubSystemApiNode
{
    public automatic_process(ModelNode parent) : base(nameof(automatic_process), parent)
    {
        public_state = PropertyUrn<ProductionState>.Build(Urn, nameof(public_state));
    }
    public PropertyUrn<ProductionState> public_state { get; }
}

public class forced_process: SubSystemApiNode
{
    public forced_process(ModelNode parent) : base(nameof(forced_process), parent)
    {
        public_state = PropertyUrn<ProductionState>.Build(Urn, nameof(public_state));
    }
    public PropertyUrn<ProductionState> public_state { get; }
}

public class mode_selector: SubSystemNode
{
    public mode_selector(ModelNode parent) : base(nameof(mode_selector), parent)
    {
    }
}

public class control_panel : SubSystemNode
{
    public control_panel(ModelNode parent) : base(nameof(control_panel), parent)
    {
       public_state =  PropertyUrn<ControlPublicState>.Build(this.Urn, nameof(public_state));
       mode = UserSettingUrn<Mode>.Build(this.Urn, nameof(mode));
    }

    public PropertyUrn<ControlPublicState> public_state { get; }

    public UserSettingUrn<Mode> mode { get; }


}

public class tank: ModelNode
{
    public tank(ModelNode parent) : base(nameof(tank), parent)
    {
        liquid_temperature = new MeasureNode<Temperature>(nameof(liquid_temperature), this);
        liquid_level = new MeasureNode<Percentage>(nameof(liquid_level), this);
        min_level = UserSettingUrn<Percentage>.Build(Urn, nameof(min_level));
        max_level = UserSettingUrn<Percentage>.Build(Urn, nameof(max_level));

    }
    
    public MeasureNode<Temperature> liquid_temperature { get; }
    public MeasureNode<Percentage>  liquid_level { get; }

    public UserSettingUrn<Percentage> min_level { get; }
    public UserSettingUrn<Percentage> max_level { get; }
}

[ValueObject]
public enum ProductionState
{
    Stop,
    Filling,
    Heating,
    Emptying,
}

[ValueObject]
public enum Mode
{
    Automatic = 0,
    ForceEmptying = 1,
    ForceFilling = 2
}

[ValueObject]
public enum ControlPublicState
{
    Stop = 0,
    Automatic = 1,
    Manual = 2
}
