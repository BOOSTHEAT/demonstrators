using System.Drawing;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Training.GuiTricks.Tricks;

public class InactiveOrInvisibleButtons : Trick
{
  public override string Title => "Inactive or Invisible Buttons";

  [ValueObject]
  public enum ButtonBehavior
  {
    Normal,
    Inactive,
    Invisible,
    Meow
  }

  public VersionSettingUrn<ButtonBehavior> behavior { get; }
  public VersionSettingUrn<Pressure> current_result { get; }

  public InactiveOrInvisibleButtons(ModelNode parent, string urnToken) : base(parent, urnToken)
  {
    behavior = VersionSettingUrn<ButtonBehavior>.Build(Urn, nameof(behavior));
    current_result = VersionSettingUrn<Pressure>.Build(Urn, nameof(current_result));
    Data = new (Urn Urn, string Value)[]
    {
      (behavior, ((int)ButtonBehavior.Normal).ToString()),
      (current_result, "0"),
    };
  }

  class Display : Screens
  {
    public IEnumerable<AlignedBlock> Content(InactiveOrInvisibleButtons _)
    {
      yield return At.Left(10).Top(10).Put(
        Column.Spacing(5).Layout(
          Label(_.Title).With(Font.Size(20).ExtraBold),
          Row.Layout(Label("Use the big button to increment the value:"), Show(_.current_result)),
          Label("The button state depends on the choice below"),
          DropDownList(_.behavior)
        )
      );
      yield return At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(
        Switch
          .Case(Value(_.behavior) == ButtonBehavior.Normal,
            Background(Box.Width(200).Height(50).Fill(Color.Chocolate)).Layout(
              At.HorizontalCenterOffset(0).VerticalCenterOffset(0)
                .Put(Label("Click Me").With(Font.Color(Color.Black).Size(40)))
            ).Increment(_.current_result, 1.0)
          )
          .Case(Value(_.behavior) == ButtonBehavior.Inactive,
            Background(Box.Width(200).Height(50).Fill(Color.BurlyWood)).Layout(
              At.HorizontalCenterOffset(0).VerticalCenterOffset(0)
                .Put(Label("Click Me").With(Font.Color(Color.Gray).Size(40)))
            )
          )
          .Case(Value(_.behavior) == ButtonBehavior.Invisible, Box)
          .Default(Label("Curiosity killed the cat 🐱").With(Font.Size(20).ExtraBold))
      );
    }
  }

  public override IEnumerable<AlignedBlock> Content => new Display().Content(this);
  public override IEnumerable<(Urn Urn, string Value)> Data { get; }
}