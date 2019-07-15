using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraMap;
using Subway.Data;
using System.Diagnostics;
using DevExpress.XtraCharts;

namespace Subway
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {

        public XtraForm1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //InitChartControl();
            InitMapControl();
            InitPieChartControl();
        }

        private void InitMapControl()
        {
            VectorItemsLayer itemsLayer = new VectorItemsLayer();
            MapItemStorage storage = new MapItemStorage();
            List<MapItem> stations = Getstations();
            storage.Items.AddRange(stations);
            itemsLayer.Data = storage;
            map.Layers.Add(itemsLayer);
        }

        private void InitChartControl()
        {
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "요일별 승하차 인원";
            chartControl1.Titles.Add(chartTitle1);

            UpdateSeries(null);
        }

        #region TODO: 역별 승하차 승객 수 순위 파이차트
        private void InitPieChartControl()
        {
            ChartControl chartControl2 = new ChartControl();
            // Create a doughnut series. 
            Series series1 = new Series("승차", ViewType.Doughnut);

            // Populate the series with points. 
            series1.Points.Add(new SeriesPoint("Russia", 17.0752));
            series1.Points.Add(new SeriesPoint("Canada", 9.98467));
            series1.Points.Add(new SeriesPoint("USA", 9.63142));
            series1.Points.Add(new SeriesPoint("China", 9.59696));
            series1.Points.Add(new SeriesPoint("Brazil", 8.511965));
            series1.Points.Add(new SeriesPoint("Australia", 7.68685));
            series1.Points.Add(new SeriesPoint("India", 3.28759));
            series1.Points.Add(new SeriesPoint("Others", 81.2));

            // Add the series to the chart. 
            chartControl2.Series.Add(series1);

            // Specify the text pattern of series labels. 
            series1.Label.TextPattern = "{A}: {VP:P0}";

            // Specify how series points are sorted. 
            series1.SeriesPointsSorting = SortingMode.Ascending;
            series1.SeriesPointsSortingKey = SeriesPointKey.Argument;

            // Specify the behavior of series labels. 
            ((DoughnutSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;
            ((DoughnutSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((DoughnutSeriesLabel)series1.Label).ResolveOverlappingMinIndent = 5;

            // Adjust the view-type-specific options of the series. 
            ((DoughnutSeriesView)series1.View).ExplodedPoints.Add(series1.Points[0]);
            ((DoughnutSeriesView)series1.View).ExplodedDistancePercentage = 30;

            // Access the diagram's options. 
            ((SimpleDiagram)chartControl2.Diagram).Dimension = 2;

            // Add a title to the chart and hide the legend. 
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = "역별 승차 인원 순위";
            chartControl2.Titles.Add(chartTitle1);
            chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

        }
        #endregion 

        private void UpdateSeries(string station)
        {
            Series series1 = new Series("승차", ViewType.Bar);
            Series series2 = new Series("하차", ViewType.Bar);
            Series series3 = new Series("합계", ViewType.Line);
            List<int?> passengers;
            if (station == null)
                passengers = DataRepository.Daejeon.GetPassengers();
            else
                passengers = DataRepository.Daejeon.GetPassengers(x => x.역명.Equals(station));

            string[] dayOfWeek = { "일", "월", "화", "수", "목", "금", "토" };
            for (int i = 0; i < passengers.Count(); i += 2)
            {
                int idx = i / 2;
                series1.Points.Add(new SeriesPoint(dayOfWeek[idx], passengers[i]));
                series2.Points.Add(new SeriesPoint(dayOfWeek[idx], passengers[i + 1]));
                series3.Points.Add(new SeriesPoint(dayOfWeek[idx], passengers[i] + passengers[i + 1]));
            }

            chartControl1.SeriesSerializable = new Series[] { series1, series2, series3 };
        }

        // Create an array of callouts
        List<MapItem> Getstations()
        {
            List<string> stations = DataRepository.Daejeon.GetStationNames();
            double mid = stations.Count()/2;

            GeoPoint start = new GeoPoint(mid, 0);
            GeoPoint end = new GeoPoint(mid - stations.Count() + 1, 0);

            List<MapItem> mapItems = new List<MapItem>();
            mapItems.Add(new MapLine { Point1 = start, Point2 = end, Stroke = Color.Green, StrokeWidth = 3 });
            for (int i = 0; i < stations.Count(); i++)
                mapItems.Add(new MapCallout() { Text = stations[i], Location = new GeoPoint((mid-i), 0) });

            return mapItems;
        }


        private void Map_SelectionChanged(object sender, MapSelectionChangedEventArgs e)
        {
            var selectedStation = e.Selection.Count > 0 ? e.Selection[0] as 
                MapCallout : null;
            if (selectedStation == null)
                UpdateSeries(null);
            else
                UpdateSeries(selectedStation.Text);
        }
    }
}