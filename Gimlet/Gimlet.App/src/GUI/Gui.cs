using System.Drawing;
using System.Linq;
using System.Reflection;
using Gimlet.Model;
using ImpliciX.Language.GUI;

namespace Gimlet.App.GUI;

public class Gui : Screens
{
  public static ImpliciX.Language.GUI.GUI Definition()
  {
    var selected = Box.Radius(2).Border(Color.Orange);
    var menuFont = Font.Medium.Size(24);
    var menu = At.Right(20).Top(60)
      .Put( Column.Spacing(15)
      .Layout( Label(" Home ").With(menuFont).NavigateTo(general.main_screen,selected),
               Label(" State ").With(menuFont).NavigateTo(general.pie_screen,selected),
               Label(" KPI ").With(menuFont).NavigateTo(general.kpi1_screen,selected),
               Label(" Report ").With(menuFont).NavigateTo(general.all_production_reports_screen,selected)
               ));
    
    return GUI
      .Assets(Assembly.GetExecutingAssembly())
      .ScreenSize(800,480)
      .Locale(device._.locale)
      .TimeZone(device._.timezone)
      .Translations("Assets.translations.csv")
      .StartWith(general.main_screen)
      .Screen(general.main_screen, Home.Screen().Append(menu).ToArray())
      .Screen(general.pie_screen, StatePie.Screen().Append(menu).ToArray())
      .Screen(general.kpi1_screen, Kpi.Screen().Append(menu).ToArray())
      .Screen(general.all_production_reports_screen, ProductionReports.MainScreen().Append(menu).ToArray())
      .Screen(general.create_production_report_screen, ProductionReports.NewReportScreen().ToArray())
        ;

  }

}