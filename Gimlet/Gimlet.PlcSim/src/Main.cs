using System.Reflection;
using ImpliciX.Language;
using ImpliciX.Language.Control;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;
using ImpliciX.Language.Store;

namespace Gimlet.PlcSim;

public sealed class Main : ApplicationDefinition
{
  public Main()
  {
    AppName = "Gimlet.PlcSim";
    AppSettingsFile = "appsettings.json";

    DataModelDefinition = ModuleDefinition.DataModel(device._);

    ModuleDefinitions = new object[]
    {
      new TcpModbusApiModuleDefinition()
      {
        Presence = device._.modbusapi_presence,
        AlarmsMap = new Dictionary<Urn, ushort>(),
        MeasuresMap = new Dictionary<Urn, ushort>()
          .Map(data.filling_valve.switcher, 100)
          .Map(data.filling_valve.status, 4)
          .Map(data.heater.switcher, 103)
          .Map(data.heater.status, 7)
          .Map(data.tank.temperature, 0)
          .Map(data.tank.level, 2)
          .Map(data.brewer.switcher, 102)
          .Map(data.brewer.status, 6)
          .Map(data.emptying_valve.switcher, 101)
          .Map(data.emptying_valve.status, 5),
      },
      
      new UserInterfaceModuleDefinition { UserInterface = Gui.Definition },

      new ControlModuleDefinition
      {
        Assembly = Assembly.GetExecutingAssembly()
      },
  
      new PersistentStoreModuleDefinition
      {
        DefaultUserSettings = DefaultValues.UserSettings,
        DefaultVersionSettings = DefaultValues.VersionSettings,
        CleanVersionSettings = device._._clean_version_settings
      },

      ModuleDefinition.SystemSoftware(device._, s => s == AppName),
      ModuleDefinition.MmiHost(device._),
    };
  }
  
}

public static class Extensions
{
  public static Dictionary<Urn, ushort> Map<T>(this Dictionary<Urn, ushort> @this,
    PropertyUrn<T> urn, ushort register)
  {
    @this.Add(urn, register);
    return @this;
  }
  public static Dictionary<Urn, ushort> Map<T>(this Dictionary<Urn, ushort> @this,
    CommandUrn<T> urn, ushort register)
  {
    @this.Add(urn, register);
    return @this;
  }
}