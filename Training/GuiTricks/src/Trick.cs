using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Training.GuiTricks;

public abstract class Trick : GuiNode
{
  public abstract string Title { get; }
  public abstract IEnumerable<AlignedBlock> Content { get; }
  public abstract IEnumerable<(Urn Urn,string Value)> Data { get; }

  protected Trick(ModelNode parent, string urnToken) : base(parent, urnToken)
  {
  }
}