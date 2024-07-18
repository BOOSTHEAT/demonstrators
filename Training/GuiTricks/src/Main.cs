using System.Drawing;
using System.Reflection;
using ImpliciX.Language;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using ImpliciX.Language.Store;

namespace Training.GuiTricks;

public class Main : ApplicationDefinition
{
  public Main()
  {
    AppName = "Training GuiTricks";
    AppSettingsFile = "appsettings.json";

    DataModelDefinition = new DataModelDefinition
    {
      Assembly = Assembly.GetExecutingAssembly()
    };

    ModuleDefinitions = new object[]
    {
      new UserInterfaceModuleDefinition { UserInterface = () => MakeGui.From(root.Tricks) },
      
      new PersistentStoreModuleDefinition
      {
        DefaultVersionSettings = root.Tricks.SelectMany(trick => trick.Data)
          .ToDictionary(x => x.Urn, x => new[] { ("value", x.Value) }),
        DefaultUserSettings = new Dictionary<Urn, (string Name, string Value)[]>(),
      },
    };
  }

  class MakeGui : Screens
  {
    public static GUI From(params Trick[] tricks)
    {
      var gui = GUI
          .Assets(Assembly.GetExecutingAssembly())
          .StartWith(root.main_screen)
          .Screen(root.main_screen, Screen(
            tricks.Select((trick,i) =>
              At.Left(0).Top(i*35)
                .Put(Helpers.CreateButton(200,30, Color.Salmon, trick.Title, trick))
            ).ToArray()
          ));
      var homeButton = At.Top(0).Right(0)
        .Put(Helpers.CreateButton(80,30, Color.Silver, "Home", root.main_screen));
      foreach (var trick in tricks)
        gui = gui.Screen(trick, Screen(trick.Content.Append(homeButton).ToArray()));
      return gui;
    }
  }

}