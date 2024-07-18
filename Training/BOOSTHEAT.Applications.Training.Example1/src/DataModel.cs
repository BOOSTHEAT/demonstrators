using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace BOOSTHEAT.Applications.Training.Example1;

public class training : RootModelNode
{
  static training()
  {
    var self = new training();

    pump_control = new SubSystemNode(nameof(pump_control), self);
    running_threshold = VersionSettingUrn<Temperature>.Build(self.Urn, nameof(running_threshold));
    stopping_threshold = VersionSettingUrn<Temperature>.Build(self.Urn, nameof(stopping_threshold));
    calculation = VersionSettingUrn<FunctionDefinition>.Build(self.Urn, nameof(calculation));

    io_board = new HardwareAndSoftwareDeviceNode(nameof(io_board), self);
    return_temperature = new MeasureNode<Temperature>(nameof(return_temperature), self);
    pump_power = CommandNode<PowerSupply>.Create("PUMP_POWER", self);
    pump_throttle = CommandNode<Percentage>.Create("PUMP_THROTTLE", self);
    pump_power_consumption = new MeasureNode<Power>(nameof(pump_power_consumption), self);

    main_screen = new GuiNode(self, nameof(main_screen));
    details_screen = new GuiNode(self, nameof(details_screen));

    thingsboard = new thingsboard(self);
  }

  public training() : base(nameof(training))
  {
  }

  public static SubSystemNode pump_control { get; }
  public static VersionSettingUrn<Temperature> running_threshold { get; }
  public static VersionSettingUrn<Temperature> stopping_threshold { get; }
  public static VersionSettingUrn<FunctionDefinition> calculation { get; }

  public static HardwareAndSoftwareDeviceNode io_board { get; }
  public static MeasureNode<Temperature> return_temperature { get; }
  public static CommandNode<PowerSupply> pump_power { get; }
  public static CommandNode<Percentage> pump_throttle { get; }
  public static MeasureNode<Power> pump_power_consumption { get; }

  public static GuiNode main_screen { get; }
  public static GuiNode details_screen { get; }

  public static thingsboard thingsboard { get; }
}

public class thingsboard : ModelNode
{
  public thingsboard(ModelNode parent) : base(nameof(thingsboard), parent)
  {
    host = UserSettingUrn<Literal>.Build(Urn, nameof(host));
    access_token = UserSettingUrn<Literal>.Build(Urn, nameof(access_token));
    delay_before_retry = VersionSettingUrn<Duration>.Build(Urn, nameof(delay_before_retry));
    delay_before_enable = VersionSettingUrn<Duration>.Build(Urn, nameof(delay_before_enable));
  }

  public UserSettingUrn<Literal> host { get; }
  public UserSettingUrn<Literal> access_token { get; }
  public VersionSettingUrn<Duration> delay_before_retry { get; }
  public VersionSettingUrn<Duration> delay_before_enable { get; }
}