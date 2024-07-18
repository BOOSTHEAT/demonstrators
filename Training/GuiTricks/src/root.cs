using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;
using Training.GuiTricks.Tricks;

namespace Training.GuiTricks;

public class root : RootModelNode
{
  static root()
  {
    var self = new root();
    main_screen = new GuiNode(self, nameof(main_screen));
    inactive_or_invisible_button = new InactiveOrInvisibleButtons(self,nameof(inactive_or_invisible_button));
    Tricks = new Trick[]
    {
      inactive_or_invisible_button
    };
  }

  private root() : base(nameof(root))
  {
  }

  public static GuiNode main_screen { get; }
  public static InactiveOrInvisibleButtons inactive_or_invisible_button { get; }

  public static Trick[] Tricks;

}