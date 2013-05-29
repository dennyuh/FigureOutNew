namespace AutoFigPro
{
    partial class ChartWin
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartCtrl = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuLabel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDataPoint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartCtrl)).BeginInit();
            this.contextMenuLabel.SuspendLayout();
            this.contextMenuDataPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartCtrl
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.Title = "XX";
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.Name = "ChartArea1";
            this.chartCtrl.ChartAreas.Add(chartArea1);
            this.chartCtrl.Cursor = System.Windows.Forms.Cursors.Default;
            this.chartCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCtrl.Location = new System.Drawing.Point(0, 0);
            this.chartCtrl.Name = "chartCtrl";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.LabelBackColor = System.Drawing.Color.White;
            series1.Name = "MainData";
            series1.SmartLabelStyle.Enabled = false;
            this.chartCtrl.Series.Add(series1);
            this.chartCtrl.Size = new System.Drawing.Size(822, 479);
            this.chartCtrl.TabIndex = 0;
            this.chartCtrl.Text = "FigureOut";
            this.chartCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartCtrl_MouseDown);
            this.chartCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartCtrl_MouseMove);
            // 
            // contextMenuLabel
            // 
            this.contextMenuLabel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteLabelToolStripMenuItem});
            this.contextMenuLabel.Name = "contextMenuStrip1";
            this.contextMenuLabel.Size = new System.Drawing.Size(139, 26);
            // 
            // deleteLabelToolStripMenuItem
            // 
            this.deleteLabelToolStripMenuItem.Name = "deleteLabelToolStripMenuItem";
            this.deleteLabelToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.deleteLabelToolStripMenuItem.Text = "Delete Label";
            this.deleteLabelToolStripMenuItem.Click += new System.EventHandler(this.contextDeleteLabel_Click);
            // 
            // contextMenuDataPoint
            // 
            this.contextMenuDataPoint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addLabelToolStripMenuItem});
            this.contextMenuDataPoint.Name = "contextMenuStrip2";
            this.contextMenuDataPoint.Size = new System.Drawing.Size(128, 26);
            // 
            // addLabelToolStripMenuItem
            // 
            this.addLabelToolStripMenuItem.Name = "addLabelToolStripMenuItem";
            this.addLabelToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.addLabelToolStripMenuItem.Text = "Add Label";
            this.addLabelToolStripMenuItem.Click += new System.EventHandler(this.contextAddLabel_Click);
            // 
            // ChartWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 479);
            this.Controls.Add(this.chartCtrl);
            this.Name = "ChartWin";
            this.Text = "ChartWin";
            ((System.ComponentModel.ISupportInitialize)(this.chartCtrl)).EndInit();
            this.contextMenuLabel.ResumeLayout(false);
            this.contextMenuDataPoint.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chartCtrl;
        private System.Windows.Forms.ContextMenuStrip contextMenuLabel;
        private System.Windows.Forms.ToolStripMenuItem deleteLabelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataPoint;
        private System.Windows.Forms.ToolStripMenuItem addLabelToolStripMenuItem;
    }
}