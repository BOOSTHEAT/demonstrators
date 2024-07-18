using System.Drawing;
using ImpliciX.Language.GUI;

namespace Training.GuiTricks;

public class Helpers : Screens
{
  public static Block CreateButton(int width, int height, Color color, string text, GuiNode target)
  {
    return Background(Box.Width(width).Height(height).Fill(color)).Layout(
      At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(Label(text))
    ).NavigateTo(target, Box);
  }

}