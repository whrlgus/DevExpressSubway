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

            // Create a layer to load image tiles from OpenStreetMap. 
            //ImageLayer tileLayer = new ImageLayer();
            //tileLayer.DataProvider = new OpenStreetMapDataProvider();
            //map.Layers.Add(tileLayer);

            // Create a layer to display vector items. 
            VectorItemsLayer itemsLayer = new VectorItemsLayer();
            map.Layers.Add(itemsLayer);

            // Create a storage for map items and generate them. 
            MapItemStorage storage = new MapItemStorage();

            List<MapItem> stations = Getstations();
            storage.Items.AddRange(stations);
            itemsLayer.Data = storage;
        }
        

        // Create an array of callouts
        List<MapItem> Getstations()
        {
            List<string> stations = DataRepository.Daejeon.GetStation();
            List<MapItem> mapItems = new List<MapItem>();

            double mid = stations.Count()/2;
            mapItems.Add(new MapLine { Point1 = new GeoPoint(mid, 0), Point2 = new GeoPoint(mid - stations.Count()+1, 0), Stroke = Color.Yellow, StrokeWidth = 3 });
            for (int i = 0; i < stations.Count(); i++)
            {
               mapItems.Add(new MapCallout() { Text = stations[i], Location = new GeoPoint((mid-i), 0) });

                //mapItems.Add(new MapDot() { Location = new GeoPoint((i - mid) * 5, (i - mid) * 40), Size = 18, Stroke = Color.Blue });
            }
            

            return mapItems;
        }


        private void Map_SelectionChanged(object sender, MapSelectionChangedEventArgs e)
        {
            var selectedStation = e.Selection.Count > 0 ? e.Selection[0] as 
                MapCallout : null;
            if (selectedStation == null) return;
            string station = selectedStation.Text;

        }
    }
}