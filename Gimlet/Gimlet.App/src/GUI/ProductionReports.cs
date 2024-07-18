using System.Collections.Generic;
using System.Drawing;
using Gimlet.Model;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Gimlet.App.GUI;

public class ProductionReports : Screens
{
  public static IEnumerable<AlignedBlock> MainScreen()
  {
    yield return At.Left(10).Top(10).Put(Translate("ProductionReports").With(Font.ExtraBold.Size(24)));
    yield return At.Left(10).Top(40).Put(_reportsGrid);

    yield return At.Right(30).Bottom(30).Put(
      Background(Box.Width(50).Height(50).Fill(Color.Gray)).Layout(
        At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(Label("+").With(Font.Size(48)))
      ).NavigateTo(general.create_production_report_screen, Box));
  }

  private static readonly Block _reportsGrid =
    Background(Box.Width(650).Height(400).Fill(Color.Chocolate))
      .Layout(At.Left(30).Top(20).Put(
        Column.Spacing(5).Layout(
          _reportsGrid_Row(0),
          _reportsGrid_Row(1),
          _reportsGrid_Row(2),
          _reportsGrid_Row(3),
          _reportsGrid_Row(4)
        ))
      );

  private static Block _reportsGrid_Row(int recordIndex)
  {
    var font = Font.Medium.Size(24);
    var title = PropertyUrn<Literal>.Build(general.production_report.Urn, recordIndex.ToString(), nameof(ProductionReport.title));

    return Row.Layout(
      Label($"Record {recordIndex} Title = ").With(font),
      Show(title).With(font)
    );
  }

  public static IEnumerable<AlignedBlock> NewReportScreen()
  {
    var bigFont = Font.ExtraBold.Size(24);
    var font = Font.Medium.Size(16);
    yield return At.Left(10).Top(10).Put(Column.Spacing(10).Layout(
        Translate("NewProductionReport").With(bigFont),
        Row.Spacing(5).Layout(
          Translate(general.production_report_input.form.title.Value).With(font),
          Input(general.production_report_input.form.title).With(font)
        ),
        Row.Spacing(5).Layout(
          Translate(general.production_report_input.form.summary.Value).With(font),
          Input(general.production_report_input.form.summary).Width(600).With(font)
        )
      )
    );

    yield return At.Right(50).Bottom(20).Put(
      Row.Spacing(30).Layout(
        Background(Box.Width(200).Height(40).Fill(Color.Gray)).Layout(
          At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(Translate("Cancel").With(bigFont))
        ).NavigateTo(general.all_production_reports_screen, Box),
        Background(Box.Width(200).Height(40).Fill(Color.Gray))
          .Layout(At.HorizontalCenterOffset(0).VerticalCenterOffset(0).Put(Translate("Confirm").With(bigFont)))
          .Send(general.production_report_input.write)
          .NavigateTo(general.all_production_reports_screen, Box)
      )
    );
  }
}