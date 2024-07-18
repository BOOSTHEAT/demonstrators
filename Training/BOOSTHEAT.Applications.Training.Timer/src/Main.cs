using System.Collections.Generic;
using ImpliciX.Language;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;
using ImpliciX.Language.Store;

namespace BOOSTHEAT.Applications.Training.Timer;

public class Main : ApplicationDefinition
{
  public Main()
  {
    AppName = "Training Timer";
    AppSettingsFile = "appsettings.json";

    DataModelDefinition = new DataModelDefinition
    {
      Assembly = typeof(_).Assembly
    };

    ModuleDefinitions = new object[]
    {
      new ControlModuleDefinition
      {
        Assembly = typeof(TimerControl).Assembly
      },
      
      new PersistentStoreModuleDefinition
      {
        DefaultVersionSettings = new Dictionary<Urn, (string Name, string Value)[]>
        {
          { _.active, new[] { ("value", "Disabled") } },
          { _.activation_delay, new[] { ("value", "10") } }
        }
      }
    };
  }
}