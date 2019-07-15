namespace Subway
{
    partial class XtraForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.map = new DevExpress.XtraMap.MapControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.map.Location = new System.Drawing.Point(12, 12);
            this.map.MaxZoomLevel = 7D;
            this.map.MinZoomLevel = 5D;
            this.map.Name = "map";
            this.map.NavigationPanelOptions.ShowCoordinates = false;
            this.map.NavigationPanelOptions.ShowKilometersScale = false;
            this.map.NavigationPanelOptions.ShowMilesScale = false;
            this.map.NavigationPanelOptions.ShowScrollButtons = false;
            this.map.NavigationPanelOptions.ShowZoomTrackbar = false;
            this.map.NavigationPanelOptions.Visible = false;
            this.map.Size = new System.Drawing.Size(399, 842);
            this.map.TabIndex = 0;
            this.map.ZoomLevel = 6D;
            this.map.SelectionChanged += new DevExpress.XtraMap.MapSelectionChangedEventHandler(this.Map_SelectionChanged);
            // 
            // chartControl1
            // 
            this.chartControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(417, 17);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.SeriesTemplate.SeriesColorizer = null;
            this.chartControl1.Size = new System.Drawing.Size(905, 838);
            this.chartControl1.TabIndex = 1;
            this.chartControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartControl1_MouseClick);
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 865);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.map);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl map;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}