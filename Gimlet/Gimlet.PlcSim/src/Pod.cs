using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using static ImpliciX.Language.Control.Condition;

namespace Gimlet.PlcSim;

public class Pod<TCommand, TStatus, TChange> : SubSystemNode
{
  public Pod(string name, ModelNode parent) : base(name, parent)
  {
    failure = UserSettingUrn<Presence>.Build(Urn, nameof(failure));
    running_change = VersionSettingUrn<TChange>.Build(Urn, nameof(running_change));
    stopped_change = VersionSettingUrn<TChange>.Build(Urn, nameof(stopped_change));
    current_change = PropertyUrn<TChange>.Build(Urn, nameof(current_change));
    switcher = CommandUrn<TCommand>.Build(Urn, nameof(switcher));
    status = VersionSettingUrn<TStatus>.Build(Urn, nameof(status));
  }
  public UserSettingUrn<Presence> failure { get; }
  public VersionSettingUrn<TChange> running_change { get; }
  public VersionSettingUrn<TChange> stopped_change { get; }
  public PropertyUrn<TChange> current_change { get; }
  public CommandUrn<TCommand> switcher { get; }
  public VersionSettingUrn<TStatus> status { get; }
}

public enum PodState
{
  Stopped,
  Running,
  StoppedFailure,
  RunningFailure
}

public class PodStateMachine<TCommand, TStatus, TChange> : SubSystemDefinition<PodState>
{
  public PodStateMachine(
    Pod<TCommand, TStatus, TChange> node,
    TCommand run,
    TCommand stop,
    TStatus running,
    TStatus stopped,
    TStatus failure
    )
  {
    // @formatter:off
    Subsystem(node)
      .Initial(PodState.Stopped)
      
      .Define(PodState.Stopped)
        .OnEntry
          .Set(node.status, stopped)
        .OnState
          .Set(node.current_change, node.stopped_change)
        .Transitions
          .When(Is(node.failure, Presence.Enabled)).Then(PodState.StoppedFailure)
          .WhenMessage(node.switcher, run).Then(PodState.Running)
      
      .Define(PodState.Running)
        .OnEntry
          .Set(node.status, running)
        .OnState
          .Set(node.current_change, node.running_change)
        .Transitions
          .When(Is(node.failure, Presence.Enabled)).Then(PodState.RunningFailure)
          .WhenMessage(node.switcher, stop).Then(PodState.Stopped)
      
      .Define(PodState.StoppedFailure)
        .OnEntry
          .Set(node.status, failure)
        .OnState
          .Set(node.current_change, node.stopped_change)
        .Transitions
          .When(Is(node.failure, Presence.Disabled)).Then(PodState.Stopped)
          .WhenMessage(node.switcher, run).Then(PodState.RunningFailure)
      
      .Define(PodState.RunningFailure)
        .OnEntry
          .Set(node.status, failure)
        .OnState
          .Set(node.current_change, node.stopped_change)
        .Transitions
          .When(Is(node.failure, Presence.Disabled)).Then(PodState.Running)
          .WhenMessage(node.switcher, stop).Then(PodState.StoppedFailure);
    // @formatter:on
  }
}

