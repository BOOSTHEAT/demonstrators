using System;
using System.Drawing;
using System.Reflection;
using Caliper.Model;
using Caliper.Model.Enums;
using ImpliciX.Language.GUI;
using ImpliciX.Language.Model;

namespace Caliper.App.GUI;

public class Gui : Screens
{
    public static ImpliciX.Language.GUI.GUI Definition()
    {

        var selected = Box.Width(40).Height(40).Border(Color.DarkGray);

        var menu = At.Right(20).Top(40)
            .Put(Column.Spacing(15).Layout(
                Label("Main Screen").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.main_screen, selected), 
                Label("Compressor status consumption").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_pie_input, selected),
                Label("Heat production").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_pie_output, selected),
                Label("Gas consumption").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_bar_input, selected),
                Label("Compressor running time").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_bar_output, selected),
                Label("Electrical consumtion").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_time_lines, selected),
                Label("Time Bars").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_time_bars, selected)
                )
            );

        var date = At.Left(10).Top(70)
            .Put(Now.Date
                .With(Font.Medium.Color(Color.White)));

        var version = At.Left(10).Top(90)
            .Put(Label("2023.7.12.4").With(Font.Medium.Color(Color.FloralWhite)));
 

        // Graphics elements

        var background = At.Left(0).Top(0)
          .Put(Image("GUI/Assets/background_loop1.png"));


        var background_secondary = At.Left(0).Top(0)
          .Put(Image("GUI/Assets/background_secondary_loop2.png"));

        var pie_icone = Image("GUI/Assets/statistiques.png");
        var bar_graph_icone = Image("GUI/Assets/graphique.png");
        var line_icone = Image("GUI/Assets/graphiques_2.png");


        //


        var energyConsumptionChart = Chart.StackedTimeBars(
                        Of(MetricUrn.Build(system.metrics.heat.heating_energy_chart, "_1Minutes")).Fill(Color.LightBlue),
                        Of(MetricUrn.Build(system.metrics.heat.dhw_energy_chart, "_1Minutes")).Fill(Color.DarkBlue)
                        ).Over.ThePast(10).Minutes;

        var energyConsumptionChart_zoom = Chart.StackedTimeBars(
                        Of(MetricUrn.Build(system.metrics.heat.heating_energy_chart, "_1Minutes")).Fill(Color.LightBlue),
                        Of(MetricUrn.Build(system.metrics.heat.dhw_energy_chart, "_1Minutes")).Fill(Color.DarkBlue)
                        ).Over.ThePast(10).Minutes;

        // Miniatures de l'écran principal
        /*
        var mini_pie_input = At.Left(144).Top(70).Put(
            Chart.Pie(
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Disabled.ToString(), "electrical_index")).Fill(Color.Chartreuse),
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Running.ToString(), "electrical_index")).Fill(Color.Red),
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Failure.ToString(), "electrical_index")).Fill(Color.OliveDrab)
                ).Width(50).Height(50).NavigateTo(general.zoom_pie_input, selected)
        );

        var mini_bar_input = At.Left(144).Top(362).Put(
            Chart.StackedTimeBars(
                Of(MetricUrn.Build(system.metrics.gas.gas_index_chart, "_1Minutes"))
                ).Width(50).Height(50).NavigateTo(general.zoom_bar_input, selected)
        );

        var mini_pie_output = At.Left(617).Top(70)
            .Put(Box.Width(50).Height(50).Border(Color.DarkGray).NavigateTo(general.zoom_pie_output, selected)
            );

        var mini_bar_output = At.Left(617).Top(362).Put(
            energyConsumptionChart.Width(50).Height(50).NavigateTo(general.zoom_bar_output, selected)
        );

        var mini_timelines = At.Left(144).Top(216).Put(
            Chart.TimeLines(
                Of(MetricUrn.Build(system.metrics.electrical.electrical_index_chart, "_1Minutes")).Fill(Color.Coral)
                ).Width(50).Height(50).NavigateTo(general.zoom_time_lines, selected)
        );

        var mini_timebars = At.Left(617).Top(216).Put(
            Chart.StackedTimeBars(
                Of(MetricUrn.Build(system.metrics.compressor.compressor_time_chart, "_1Minutes"))
                ).Width(50).Height(50).NavigateTo(general.zoom_time_bars, selected)
        );
        */

        // Barcharts energy consumed
        /*
         var chartElectricityConsumed = At.Left(60).Top(260).Put(
                 Image("GUI/Assets/electricityConsumed.gif").DataDriven(system.metrics.electrical.electrical_index, 204500, 10)
             );

         var chartGasConsumed = At.Left(90).Top(260).Put(
                 Image("GUI/Assets/gasConsumed.gif").DataDriven(system.metrics.gas.gas_index, 314, 0.1)
             ); 

         var chartEnergyConsumed = At.Left(120).Top(260).Put(
                 Image("GUI/Assets/energyConsumed.gif").DataDriven(system.metrics.consumption, 3111, 10)
             );
         */

        // Barcharts energy produced

        var chartHeatingEnergyProduced = At.Left(680).Top(260).Put(
                Image("GUI/Assets/heatingE.gif").DataDriven(system.metrics.heat.heating_energy, 3100, 1)
            );

        var chartDhwEnergyProduced = At.Left(710).Top(260).Put(
                Image("GUI/Assets/dhwE.gif").DataDriven(system.metrics.heat.dhw_energy, 0, 1)
            );

        var chartEnergyProduced = At.Left(740).Top(260).Put(
                Image("GUI/Assets/heatE.gif").DataDriven(system.metrics.heat.energy, 3100, 1)
            );


        // TimeBars


        var energyProduction_zoom = At.Right(0).VerticalCenterOffset(0).Put(
            energyConsumptionChart.Width(480).Height(480).NavigateTo(general.main_screen, selected)
        );
        
        var gasConsumption = At.Right(0).VerticalCenterOffset(0).Put(
            Chart.StackedTimeBars(
                Of(MetricUrn.Build(system.metrics.gas.gas_index_chart, "_1Minutes"))
                ).Over.ThePast(10).Minutes
                .Width(480).Height(480).NavigateTo(general.main_screen, selected)
        );

        var compressorTime = At.Right(0).VerticalCenterOffset(0).Put(
            Chart.StackedTimeBars(
                Of(MetricUrn.Build(system.metrics.compressor.compressor_time_chart, "_1Minutes")).Fill(Color.Chartreuse)
                ).Over.ThePast(10).Minutes
                .Width(480).Height(480).NavigateTo(general.main_screen, selected)
        );


        var electricityConsumption = At.Right(0).VerticalCenterOffset(0).Put(
            Chart.TimeLines(
                Of(MetricUrn.Build(system.metrics.electrical.electrical_index_chart, "_1Minutes")).Fill(Color.Coral)
                ).Over.ThePast(10).Minutes
                .Width(480).Height(480).NavigateTo(general.main_screen, selected)
        );

        // PieChart

        var pieHeatProduction = At.Right(0).VerticalCenterOffset(0).Put(
            Chart.Pie(
                Of(system.metrics.heat.heating_power_chart).Fill(Color.Red).With(Font.ExtraBold.Size(22)),
                Of(system.metrics.heat.dhw_power_chart)
                ).Width(480).Height(480)
        );

        var pieElectricalState_Zoom = At.Right(0).VerticalCenterOffset(0).Put(
            Chart.Pie(
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Disabled.ToString(), "electrical_index")).Fill(Color.Chartreuse),
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Running.ToString(), "electrical_index")).Fill(Color.Red),
                Of(MetricUrn.Build(system.metrics.electrical.heat_service_state_chart, States.Failure.ToString(), "electrical_index")).Fill(Color.Thistle)
                ).Width(480).Height(480).NavigateTo(general.main_screen, selected)
        );

        // Titres des écrans

        var titre_menu_1 = At.HorizontalCenterOffset(0).Top(40).Put(
            Label("Main Screen").With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_2 = At.Left(25).Top(140).Put(
            Label("Electrical state monitoring").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_3 = At.Left(25).Top(140).Put(
            Label("Heat production").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_4 = At.Left(25).Top(140).Put(
            Label("Gas consumption").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_5 = At.Left(25).Top(140).Put(
            Label("Compressor running time").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_6 = At.Left(25).Top(140).Put(
            Label("Electrical consumption").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_7 = At.Left(25).Top(140).Put(
            Label("Energy production").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));

        var titre_menu_8 = At.Left(25).Top(140).Put(
            Label("System Data").Width(260).With(Font.Size(35).Medium.Color(Color.FloralWhite)));


        // Data Screen

        var data_caliper = At.Right(20).Top(40)
            .Put(Column.Spacing(15).Layout(
                Label("2023.7.12.4").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.main_screen, selected),
                Label("Compressor status consumption").With(Font.Size(15).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_pie_input, selected),
                Label("Heat production").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_pie_output, selected),
                Label("Gas consumption").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_bar_input, selected),
                Label("Compressor running time").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_bar_output, selected),
                Label("Electrical consumtion").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_time_lines, selected),
                Label("Time Bars").With(Font.Size(20).Medium.Color(Color.FloralWhite)).NavigateTo(general.zoom_time_bars, selected)
                )
            );


        return GUI
            .Assets(Assembly.GetExecutingAssembly())
            .StartWith(general.main_screen)
            .Screen(general.main_screen, new[] {
                background,
                At.Left(144).Top(70).Put(pie_icone.Width(50)), At.Left(617).Top(70).Put(pie_icone.Width(50)),
                At.Left(144).Top(362).Put(bar_graph_icone.Width(50)), At.Left(617).Top(362).Put(bar_graph_icone.Width(50)),
                At.Left(144).Top(216).Put(line_icone.Width(50)), At.Left(617).Top(216).Put(line_icone.Width(50))
            })
            
            .Screen(general.zoom_pie_input, new[] {
                background_secondary, date, version, pieElectricalState_Zoom, titre_menu_2
            })

            .Screen(general.zoom_pie_output, new[] {
                background_secondary, date, version, pieHeatProduction, titre_menu_3
            })

            .Screen(general.zoom_bar_input, new[] {
                background_secondary, date, version, gasConsumption, titre_menu_4
            })

            .Screen(general.zoom_bar_output, new[] {
                background_secondary, date, version, compressorTime, titre_menu_5
            })

            .Screen(general.zoom_time_lines, new[] {
                background_secondary, date, version, electricityConsumption, titre_menu_6
            })

            .Screen(general.zoom_time_bars, new[] {
                background_secondary, date, version, energyProduction_zoom, titre_menu_7
            })

            //.Screen(general.data_screen, new[] {
            //    background_secondary, date, version, titre_menu_8
            //})

            .Group.HorizontalSwipe(general.swipe_node, general.main_screen, general.zoom_pie_input, general.zoom_pie_output, general.zoom_bar_input, general.zoom_bar_output, general.zoom_time_lines, general.zoom_time_bars)
            
            .Screen(general.screen_saver,  new []{
                At.Left(0).Top(0).Put(Image("GUI/Assets/screen_saver.png"))
            } )
            .ScreenSaver(TimeSpan.FromMinutes(5), general.screen_saver)
            
            .Screen(general.screen_wait_for_connection,  new []{
                At.Left(0).Top(0).Put(Image("GUI/Assets/background_secondary_loop3.png")),
                At.Left(350).Top(170).Put(Image("GUI/Assets/loader.gif"))
            } )
            .WhenNotConnected(general.screen_wait_for_connection)
            
            ;
    }
}