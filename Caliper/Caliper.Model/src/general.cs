using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Caliper.Model;

public class general : RootModelNode
{
  public static GuiNode swipe_node { get; }
  public static GuiNode main_screen { get; }
  public static GuiNode zoom_pie_input { get; }
  public static GuiNode zoom_pie_output { get; }
  public static GuiNode zoom_bar_input { get; }
  public static GuiNode zoom_bar_output { get; }
  public static GuiNode zoom_time_lines { get; }
  public static GuiNode zoom_time_bars { get; }
  public static GuiNode data_screen { get; }

  public static GuiNode screen_saver { get; }
  public static GuiNode screen_wait_for_connection { get; }

  static general()
  {
    main_screen = new GuiNode(new general(), nameof(main_screen));
    zoom_pie_input = new GuiNode(new general(), nameof(zoom_pie_input));
    zoom_pie_output = new GuiNode(new general(), nameof(zoom_pie_output));
    zoom_bar_input = new GuiNode(new general(), nameof(zoom_bar_input));
    zoom_bar_output = new GuiNode(new general(), nameof(zoom_bar_output));
    zoom_time_lines = new GuiNode(new general(), nameof(zoom_time_lines));
    zoom_time_bars = new GuiNode(new general(), nameof(zoom_time_bars));
    data_screen = new GuiNode(new general(), nameof(data_screen));

    swipe_node = new GuiNode(new general(), nameof(swipe_node));

    screen_saver = new GuiNode(new general(), nameof(screen_saver));
    screen_wait_for_connection = new GuiNode(new general(), nameof(screen_wait_for_connection));
  }

  private general() : base(nameof(general))
  {
  }
}