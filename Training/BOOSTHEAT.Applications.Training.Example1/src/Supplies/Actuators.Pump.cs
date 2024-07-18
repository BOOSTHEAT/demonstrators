using System;
using ImpliciX.Language.Core;
using ImpliciX.Language.Driver;
using ImpliciX.Language.Modbus;
using ImpliciX.Language.Model;

namespace BOOSTHEAT.Applications.Training.Example1.Supplies;

public class Pump
{
  private readonly ushort _powerRegister;
  private readonly ushort _pwmRegister;
  private readonly Urn _pumpUrn;
  private readonly Func<Percentage, ushort> _pwm;


  public Pump(ushort powerRegister, ushort pwmRegister, Urn pumpUrn, Func<Percentage, ushort> pwm)
  {
    _powerRegister = powerRegister;
    _pwmRegister = pwmRegister;
    _pumpUrn = pumpUrn;
    _pwm = pwm;
  }

  public Result<Command> Power(object arg, TimeSpan currentTime, IDriverState state) =>
    from position in arg.RCast<PowerSupply>()
    let cmd = position switch
    {
      PowerSupply.On => Command.Create(_powerRegister, new ushort[] { 1 }),
      PowerSupply.Off => Command.Create(_powerRegister, new ushort[] { 0 }),
      _ => throw new NotImplementedException()
    }
    let newState = state.New(_pumpUrn)
      .WithValue(nameof(PowerSupply), arg)
      .WithValue("at", currentTime)
    select cmd.WithState(newState);

  public Result<Command> Throttle(object arg, TimeSpan _, IDriverState __) =>
    from pct in arg.RCast<Percentage>()
    let cmd = Command.Create(_pwmRegister, new[] { _pwm(pct) })
    select cmd;
}

public static class PumpExtensions
{
  public static ICommandMap Add(this ICommandMap commandMap, Pump pump, CommandNode<PowerSupply> power,
    CommandNode<Percentage> throttle)
  {
    commandMap.Add(power, pump.Power);
    commandMap.Add(throttle, pump.Throttle);
    return commandMap;
  }
}

public class GrundfosPump : Pump
{
  public GrundfosPump(ushort powerRegister, ushort pwmRegister, Urn pumpUrn)
    : base(powerRegister, pwmRegister, pumpUrn, pct => Convert.ToUInt16((84f - 74f * pct.ToFloat()) * 10))
  {
  }
}