using ImpliciX.Language.Model;
using ImpliciX.Language.StdLib;

namespace Gimlet.Model;

public class device : Device
{
    public static device _ { get; } = new ();

    private device() : base(nameof(device))
    {
        plc = new HardwareDeviceNode(nameof(plc), this);
    }
    public HardwareDeviceNode plc { get; }
}