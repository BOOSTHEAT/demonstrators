using Caliper.Model.Tree;
using ImpliciX.Language.Control;
using ImpliciX.Language.Model;

namespace Caliper.Model;

public class system : RootModelNode
{
    static system()
    {
        var rootNode = new system();

        metrics = new metrics(nameof(metrics), rootNode);
        processing = new processing(nameof(processing), rootNode);
        
    }


    private system() : base(nameof(system))
    {
    }
    public static metrics metrics { get; }
    public static processing processing { get; }
}