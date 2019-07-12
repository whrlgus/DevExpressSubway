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

namespace Subway
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {

        public XtraForm1()
        {
            InitializeComponent();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a map control with initial settings and add it to the form. 
            //MapControl map = new MapControl()
            //{ Dock = DockStyle.Fill, ZoomLevel = 1, EnableZooming=false, CenterPoint = new GeoPoint(0,0) };
            //this.Controls.Add(map);

            // Create a layer to load image tiles from OpenStreetMap. 
            ImageLayer tileLayer = new ImageLayer();
            tileLayer.DataProvider = new OpenStreetMapDataProvider();
            map.Layers.Add(tileLayer);

            // Create a layer to display vector items. 
            VectorItemsLayer itemsLayer = new VectorItemsLayer();
            map.Layers.Add(itemsLayer);

            // Create a storage for map items and generate them. 
            MapItemStorage storage = new MapItemStorage();
            
            List<MapItem> stations = Getstations();
            storage.Items.AddRange(stations);
            itemsLayer.Data = storage;
        }

        // Create an array of callouts for 5 capital cities. 
        List<MapItem> Getstations()
        {

            
            List<string> stations = DaejeonData.GetStation();
            List<MapItem> mapItems = new List<MapItem>();

            

            double mid = stations.Count()/2;
            mapItems.Add(new MapLine { Point1 = new GeoPoint(mid, 0), Point2 = new GeoPoint(mid - stations.Count() - 2, 0), Stroke = Color.Yellow, StrokeWidth = 3 });
            for (int i = 0; i < stations.Count(); i++)
            {
               mapItems.Add(new MapCallout() { Text = stations[i], Location = new GeoPoint((mid-i),0) });

                //mapItems.Add(new MapDot() { Location = new GeoPoint((i - mid) * 5, (i - mid) * 40), Size = 18, Stroke = Color.Blue });
            }
            

            return mapItems;
        }


    }
}