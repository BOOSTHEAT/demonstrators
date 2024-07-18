using System.Drawing;
using System.Reflection;
using ImpliciX.Language.GUI;

namespace BOOSTHEAT.Applications.Training.Example1;

public class Gui : Screens
{
  public static GUI Definition()
  {
    var selected = Box.Radius(12).Border(Color.DarkBlue);

    var menu = At.Right(40).Top(50).Put(
      Column.Spacing(30).Layout(
        Image("assets/menu/main.png").NavigateTo(training.main_screen,selected),
        Image("assets/menu/details.png").NavigateTo(training.details_screen,selected)
      ));
  
    var time = At.Right(10).Bottom(10)
      .Put(Now.HoursMinutesSeconds.With(Font.Light.Size(26).Color(Color.Black)));

    return GUI
      .Assets(Assembly.GetExecutingAssembly())
      .StartWith(training.main_screen)
      
      .Screen(training.main_screen, Screen(
        At.Left(50).Top(18).Put(
          Switch
            .Case(Value(training.pump_control.state) == PumpControl.State.Running,
              Image("assets/state/running.gif")
            )
            .Default(Image("assets/state/stopped.png"))
        ),
        menu,
        time
      ))
      
      .Screen(training.details_screen, Screen(
        At.Left(20).Top(50).Put(
          Row.Layout(
            Label("Return Temperature: ").With(Font.Light),
            Show(training.return_temperature).With(Font.Medium),
            Label(" °C").With(Font.Light)
          ).With(Font.Size(25).Color(Color.Black))
        ),
        At.Left(20).Top(80).Put(
          Row.Layout(
            Label("Power Consumption: ").With(Font.Light),
            Show(training.pump_power_consumption).With(Font.Medium),
            Label(" kW").With(Font.Light)
          ).With(Font.Size(25).Color(Color.Black))
        ),
        At.Left(20).Top(110).Put(
          Row.Layout(
            Label("Pump State: ").With(Font.Light),
            Show(training.pump_control.state).With(Font.Medium)
          ).With(Font.Size(25).Color(Color.Black))
        ),
        menu,
        time
      ));
  }
}