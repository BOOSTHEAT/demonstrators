using Caliper.Model.Tree;
using ImpliciX.Language.Model;

namespace Caliper.Model;

public class monitoring : RootModelNode
{
    public static heat_counter heating { get; }
    public static heat_counter dhw { get;  }
    public static product product { get; }
    public static pulse_counter pulse_counter { get; }
    static monitoring()
    {
        var devicesRoot = new monitoring();
        pulse_counter = new pulse_counter(nameof(pulse_counter), devicesRoot);
        product = new product(nameof(product), devicesRoot);
        heating = new heat_counter(nameof(heating), devicesRoot);
        dhw = new heat_counter(nameof(dhw), devicesRoot);
    }


    private monitoring() : base(nameof(monitoring))
    {
    }
}