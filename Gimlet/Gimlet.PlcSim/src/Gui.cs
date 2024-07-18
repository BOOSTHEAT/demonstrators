using System.Reflection;
using ImpliciX.Language.GUI;

namespace Gimlet.PlcSim;

public class Gui : Screens
{
  public static GUI Definition()
  {
    return GUI
      .Assets(Assembly.GetExecutingAssembly())
      .ScreenSize(800, 480)
      .Locale(device._.locale)
      .TimeZone(device._.timezone)
      .StartWith(data.main_screen)
      .Screen(data.main_screen, MainScreen().ToArray());
  }

  public static IEnumerable<AlignedBlock> MainScreen()
  {
    yield return At.Left(10).Top(10)
      .Put(Label("Gimlet I/O simulation")
        .With(Font.Medium));

    yield return At.Right(10).Top(10)
      .Put(Show(device._.software.version.measure)
        .With(Font.Medium));

    yield return At.Right(10).Top(30)
      .Put(Now.Date
        .With(Font.Light));

    yield return At.Right(10).Top(50)
      .Put(Now.HoursMinutesSeconds
        .With(Font.Light));
  }
}