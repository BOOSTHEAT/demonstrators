using ImpliciX.Language.StdLib;

namespace Model;

public class device : Device
{
  public static device _ { get; } = new ();

  private device() : base(nameof(device))
  {
  }
}