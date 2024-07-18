using System.Collections.Generic;
using Gimlet.Model;
using ImpliciX.Language.GUI;

namespace Gimlet.App.GUI;

public class Home : Screens
{
  public static IEnumerable<AlignedBlock> Screen()
  {
    yield return At.Right(0).Top(10)
      .Put(Image("Assets/background_cuve.png"));
    
    yield return At.Left(350).Top(135)
      .Put(Image("Assets/filling.gif").DataDriven(pasteurize.tank.liquid_level, 0, 0.01));
    
    yield return At.Left(10).Top(10)
      .Put(Now.Date
        .With(Font.Light));
    
    yield return At.Left(10).Top(30)
      .Put(Now.HoursMinutesSeconds
        .With(Font.Light));
    
    yield return At.Right(10).Top(10)
      .Put(Show(device._.software.version.measure)
        .With(Font.Medium));
    
    yield return At.Right(650).Top(120)
      .Put(
        Switch
          .Case(Value(pasteurize.actuators.filling_valve.status) == ValveStatus.Opened, Image("Assets/ValvesOpen.png"))
          .Case(Value(pasteurize.actuators.filling_valve.status) == ValveStatus.Failed, Image("Assets/ValvesFail.png"))
          .Case(Value(pasteurize.actuators.filling_valve.status) == ValveStatus.Closed, Image("Assets/Valves.png"))
          .Default(Image("Assets/Valves.png")));
    
    yield return At.Right(90).Top(325)
      .Put(
        Switch
          .Case(Value(pasteurize.actuators.emptying_valve.status) == ValveStatus.Opened, Image("Assets/ValvesOpen.png"))
          .Case(Value(pasteurize.actuators.emptying_valve.status) == ValveStatus.Failed, Image("Assets/ValvesFail.png"))
          .Case(Value(pasteurize.actuators.emptying_valve.status) == ValveStatus.Closed, Image("Assets/Valves.png"))
          .Default(Image("Assets/Valves.png")));
    
    yield return At.Right(360).Top(50)
      .Put(
        Switch
          .Case(Value(pasteurize.actuators.brewer.status) == BrewerStatus.Running, Image("Assets/MotorRunning.gif"))
          .Case(Value(pasteurize.actuators.brewer.status) == BrewerStatus.Failed, Image("Assets/MotorFail.png"))
          .Case(Value(pasteurize.actuators.brewer.status) == BrewerStatus.Stopped, Image("Assets/Motor.png"))
          .Default(Image("Assets/Motor.png")));
    
    yield return At.Right(310).Top(140)
      .Put(
        Switch
          .Case(Value(pasteurize.actuators.heater.status) == HeaterStatus.Running, Image("Assets/HeaterRunning.gif"))
          .Case(Value(pasteurize.actuators.heater.status) == HeaterStatus.Failed, Image("Assets/HeaterFail.png"))
          .Case(Value(pasteurize.actuators.heater.status) == HeaterStatus.Stopped, Image("Assets/Heater.png"))
          .Default(Image("Assets/Heater.png")));
    
    yield return At.Right(470).Top(200)
      .Put(
        Show(pasteurize.tank.liquid_level).With(Font.Medium.Size(20)));
    
    yield return At.Right(345).Top(160)
      .Put(
        Show(pasteurize.tank.liquid_temperature).With(Font.Medium.Size(20)));
    
    yield return At.Left(120).Bottom(153)
      .Put(OnOff(pasteurize.run));
    
    yield return At.Left(120).Top(330).Put(
      DropDownList(pasteurize.control_panel.mode).Width(200)
    );
  }
}