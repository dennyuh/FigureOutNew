using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AutoFigPro
{
    public partial class ChartWin : Form
    {
        private int lastHitPointIdx = -1;
        private ChartElementType lastHitElementType = ChartElementType.Nothing;
        private int eventHitPointIdx = -1;
        private MainWin h_Main;
        public ChartWin(MainWin h)
        {
            h_Main = h;
            InitializeComponent();
            chartCtrl.Series["MainData"].Color = Color.Black;
            chartCtrl.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chartCtrl.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chartCtrl.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chartCtrl.ChartAreas["ChartArea1"].CursorX.LineColor = Color.Empty;
//            chartCtrl.ChartAreas["ChartArea1"].CursorY.IsUserEnabled = true;
//            chartCtrl.ChartAreas["ChartArea1"].CursorY.IsUserSelectionEnabled = true;
//            chartCtrl.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
            chartCtrl.Series["MainData"].MarkerColor = Color.Red;
            chartCtrl.Series["MainData"].MarkerSize = 10;
        }

        private void chartCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            switch (lastHitElementType)
            {
                case ChartElementType.DataPointLabel:
                    chartCtrl.Series["MainData"].Points[lastHitPointIdx].LabelForeColor = Color.Black;
                    break;
                case ChartElementType.DataPoint:
                    chartCtrl.Series["MainData"].Points[lastHitPointIdx].MarkerStyle = MarkerStyle.None;
                    break;
            }
            HitTestResult result = chartCtrl.HitTest(e.X, e.Y);
            switch (result.ChartElementType)
            {
                case ChartElementType.DataPointLabel:
                    chartCtrl.Series["MainData"].Points[result.PointIndex].LabelForeColor = Color.Red;
                    lastHitPointIdx = result.PointIndex;
                    lastHitElementType = ChartElementType.DataPointLabel;
                    break;
                case ChartElementType.DataPoint:
                    if (h_Main.ParseInst.isPeak(result.PointIndex))
                    {
                        chartCtrl.Series["MainData"].Points[result.PointIndex].MarkerStyle = MarkerStyle.Cross;
                        lastHitPointIdx = result.PointIndex;
                        lastHitElementType = ChartElementType.DataPoint;
                    }
                    break;
                default:
                    lastHitPointIdx = -1;
                    lastHitElementType = ChartElementType.Nothing;
                    break;
            }
        }

        private void chartCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (lastHitPointIdx != -1 && e.Button == MouseButtons.Right)
            {
                switch (lastHitElementType)
                {
                    case ChartElementType.DataPointLabel:
                        eventHitPointIdx = lastHitPointIdx;
                        contextMenuLabel.Show(this, e.X, e.Y);
                        break;
                    case ChartElementType.DataPoint:
                        eventHitPointIdx = lastHitPointIdx;
                        contextMenuDataPoint.Show(this, e.X, e.Y);
                        break;
                }
            }
        }

        private void contextDeleteLabel_Click(object sender, EventArgs e)
        {
            chartCtrl.Series["MainData"].Points[eventHitPointIdx].Label = "";
            h_Main.ParseInst.CurPeaksIdx.RemoveAt(h_Main.ParseInst.CurPeaksIdx.IndexOf(eventHitPointIdx));
            eventHitPointIdx = -1;
        }

        private void contextAddLabel_Click(object sender, EventArgs e)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalDigits = h_Main.getDecimalPlaces();
            chartCtrl.Series["MainData"].Points[eventHitPointIdx].Label = h_Main.ParseInst.XData[eventHitPointIdx].ToString("N", provider);
            h_Main.ParseInst.CurPeaksIdx.Add(eventHitPointIdx);
            eventHitPointIdx = -1;
        }
    }
}
