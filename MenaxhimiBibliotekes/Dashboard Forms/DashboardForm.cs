﻿using DevExpress.Charts.Native;
using DevExpress.XtraCharts;
using MenaxhimiBibliotekes.BLL;
using MenaxhimiBibliotekes.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.Dashboard_Forms
{
    public partial class DashboardForm : Form
    {
        List<Material> materialList = new List<Material>();
        List<Subscriber> subscribers;
        MaterialBLL materialBLL = new MaterialBLL();
        SubscriberBLL subscriberBLL;
        List<MonthBorrowStatistic> MonthBorrowStatistics;
        List<MaterialType> mts;
        MaterialTypeBLL mtbll;
        BorrowBLL borrbll;
        List<Material> materials;
        MaterialBLL mbll;
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            subscriberBLL = new SubscriberBLL();
            borrbll = new BorrowBLL();
            mtbll = new MaterialTypeBLL();
            mbll = new MaterialBLL();


            materials = mbll.MostBorrowedBooks();
            mts = mtbll.MostBorrowedMaterialTypes();

            MonthBorrowStatistics = borrbll.Last12MonthBorrowStatistics();

            subscribers = subscriberBLL.BestSubscribers().ToList();
            materialList = materialBLL.GetAll();

            chartMaterials.Dock = DockStyle.Fill;

            Series series = new Series("Material", ViewType.Line);
            series.DataSource = MonthBorrowStatistics;
            series.ArgumentDataMember = "Month";
            series.ValueDataMembers.AddRange("BorrowingsCount");
            chartMaterials.Series.Add(series);

            LineSeriesView view = (LineSeriesView)series.View;
            view.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.HideOverlapped;
            series.Label.TextPattern = "{V:.#}";

            // Create a chart title.
            ChartTitle chartTitle = new ChartTitle();
            chartTitle.Text = "Months Timeline";
            chartMaterials.Titles.Add(chartTitle);

            // Customize axes.
            XYDiagram diagram = chartMaterials.Diagram as XYDiagram;
            diagram.AxisX.Label.TextPattern = "{A:MMM, d (HH:mm)}";
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Hour;
            diagram.AxisX.DateTimeScaleOptions.GridSpacing = 9;
            diagram.AxisX.WholeRange.SideMarginsValue = 0.5;
            diagram.AxisY.WholeRange.AlwaysShowZeroLevel = false;

            // Hide a legend if necessary.
            chartMaterials.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;










            //SideBySide chart

            //ChartControl sideBySideBarChart = chartMaterials;

            //Series series1 = new Series("Side-by-Side Bar Series 1", ViewType.Pie3D);
            //series1.DataSource = materialList;

            //series1.ArgumentDataMember = "Title";
            //series1.ValueDataMembers.AddRange("MaterialId");


            //sideBySideBarChart.Series.Add(series1);

            //sideBySideBarChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            //((XYDiagram)sideBySideBarChart.Diagram).Rotated = true;

            //ChartTitle chartTitle1 = new ChartTitle();
            //chartTitle1.Text = "Side-by-Side Bar Chart";
            //sideBySideBarChart.Titles.Add(chartTitle1);

            //Add the chart to the form.
            //sideBySideBarChart.Dock = DockStyle.Left;











            Series serie = new Series("Materials", ViewType.Bar);
            serie.ArgumentScaleType = ScaleType.Qualitative;
            serie.ValueScaleType = ScaleType.Numerical;

            serie.ArgumentDataMember = "Title";
            serie.ValueDataMembers.AddRange("Borrowings");
            chartMostBorrowedMaterials.Series.Add(serie);
            chartMostBorrowedMaterials.DataSource = materials;





            // PIE CHART =>

            

            piechart.Titles.Add(new ChartTitle() { Text = "MaterialTypes" });

            Series series1 = new Series("Most borrowed MaterialTypes", ViewType.Pie);
            series1.DataSource = mts;
            series1.ArgumentDataMember = "_MaterialType";
            series1.ValueDataMembers.AddRange("Borrowings");

            piechart.Series.Add(series1);

            series1.LegendTextPattern = "{A}";




            ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;

            ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

            PieSeriesView myView = (PieSeriesView)series1.View;

            // Specify a data filter to explode points.
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
                DataFilterCondition.GreaterThanOrEqual, 9));
            myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
                DataFilterCondition.NotEqual, "Others"));
            myView.ExplodeMode = PieExplodeMode.UseFilters;
            myView.ExplodedDistancePercentage = 30;
            myView.RuntimeExploding = true;

            // Customize the legend.
            piechart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // Add the chart to the form.
            piechart.Dock = DockStyle.Fill;



        }

        private void ChartMaterials_Click(object sender, EventArgs e)
        {

        }

        private void Piechart_Click(object sender, EventArgs e)
        {





        }
    }
}
