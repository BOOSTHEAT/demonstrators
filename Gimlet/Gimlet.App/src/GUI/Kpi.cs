using System.Collections.Generic;
using Gimlet.Model;
using ImpliciX.Language.GUI;

namespace Gimlet.App.GUI;


public class Kpi : Screens
{
  public static IEnumerable<AlignedBlock> Screen()
  {
    yield return At.Right(0).Top(0)
      .Put(Image("Assets/Background_kpi_actuators.png"));

    
    yield return At.Left(10).Top(170)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_brewer_nominal_duration).With(Font.Medium.Size(14))));     
         
    yield return At.Left(10).Top(216)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_brewer_failed_duration).With(Font.Medium.Size(14))));

    yield return At.Left(10).Top(262)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_brewer_faults_count).With(Font.Medium.Size(14)))); 

    yield return At.Left(10).Top(308)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_brewer_mttr).With(Font.Medium.Size(14))));

    yield return At.Left(10).Top(354)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_brewer_mtbf).With(Font.Medium.Size(14))));

    
    yield return At.Right(150).Top(170)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_heater_nominal_duration).With(Font.Medium.Size(14)))); 

    yield return At.Right(150).Top(216)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_heater_failed_duration).With(Font.Medium.Size(14))));
    
    yield return At.Right(150).Top(262)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_heater_faults_count).With(Font.Medium.Size(14)))); 

    yield return At.Right(150).Top(308)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_heater_mttr).With(Font.Medium.Size(14))));

    yield return At.Right(150).Top(354)
      .Put(Row.Layout( Show(pasteurize.monitoring.kpi_heater_mtbf).With(Font.Medium.Size(14))));
  }
}