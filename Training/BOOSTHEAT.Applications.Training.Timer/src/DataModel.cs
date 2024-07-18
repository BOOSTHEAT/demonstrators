using ImpliciX.Language.Model;

namespace BOOSTHEAT.Applications.Training.Timer;

public class _ : RootModelNode
{
  static _()
  {
    var self = new _();
    timer_control = new SubSystemNode(nameof(timer_control), self);
    active = VersionSettingUrn<Presence>.Build(self.Urn, nameof(active));
    activation_delay = VersionSettingUrn<Duration>.Build(self.Urn, nameof(activation_delay));
  }

  public _() : base(nameof(_))
  {
  }

  public static SubSystemNode timer_control { get; }
  public static VersionSettingUrn<Presence> active { get; }
  public static VersionSettingUrn<Duration> activation_delay { get; }
}