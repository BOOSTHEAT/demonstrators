using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Gimlet.Model;

public class general : RootModelNode
{
  public static GuiNode main_screen { get; }
  public static GuiNode pie_screen { get; }
  public static GuiNode kpi1_screen { get; }
  public static GuiNode all_production_reports_screen { get; }
  public static GuiNode create_production_report_screen { get; }

  public static RecordsNode<ProductionReport> production_report { get; }
  public static RecordWriterNode<ProductionReport> production_report_input { get; }

  static general()
  {
    var root = new general();
    main_screen = new GuiNode(root, nameof(main_screen));
    pie_screen = new GuiNode(root, nameof(pie_screen));
    kpi1_screen = new GuiNode(root, nameof(kpi1_screen));
    all_production_reports_screen = new GuiNode(root, nameof(all_production_reports_screen));
    create_production_report_screen = new GuiNode(root, nameof(create_production_report_screen));

    production_report = new RecordsNode<ProductionReport>(nameof(production_report), root);
    production_report_input = new RecordWriterNode<ProductionReport>(nameof(production_report_input), root, (n, p) => new ProductionReport(n, p));
  }

  private general() : base(nameof(general))
  {
  }
}

public class ProductionReport : ModelNode
{
  public ProductionReport(string name, ModelNode parent) : base(name, parent)
  {
    title = PropertyUrn<Literal>.Build(Urn, nameof(title));
    summary = PropertyUrn<Literal>.Build(Urn, nameof(summary));
  }

  public PropertyUrn<Literal> title { get; }
  public PropertyUrn<Literal> summary { get; }
}