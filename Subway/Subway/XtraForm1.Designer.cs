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
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            this.SuspendLayout();
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.map.Location = new System.Drawing.Point(9, 9);
            this.map.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.map.MaxZoomLevel = 7D;
            this.map.MinZoomLevel = 5D;
            this.map.Name = "map";
            this.map.NavigationPanelOptions.ShowCoordinates = false;
            this.map.NavigationPanelOptions.ShowKilometersScale = false;
            this.map.NavigationPanelOptions.ShowMilesScale = false;
            this.map.NavigationPanelOptions.ShowScrollButtons = false;
            this.map.NavigationPanelOptions.ShowZoomTrackbar = false;
            this.map.NavigationPanelOptions.Visible = false;
            this.map.Size = new System.Drawing.Size(299, 608);
            this.map.TabIndex = 0;
            this.map.ZoomLevel = 6D;
            this.map.SelectionChanged += new DevExpress.XtraMap.MapSelectionChangedEventHandler(this.Map_SelectionChanged);
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 625);
            this.Controls.Add(this.map);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl map;
    }
}