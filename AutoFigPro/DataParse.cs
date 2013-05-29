using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoFigPro
{
    public class DataParse
    {
//        private int size;
//        private List<double> xdata;
//        private List<double> ydata;
//        private List<double> ypercetagedata;
//        private double ymax;
//        private double xmin;
//        private double xmax;
        public int Size { private set; get; }
        public List<double> XData { private set; get; }
        public List<double> YData { private set; get; }
        public List<double> YPercentageData { private set; get; }
        public List<double> XExtremeData { private set; get; }
        public List<double> YExtremeData { private set; get; }
        public List<double> XPeaks { private set; get; }
        public List<double> YPeaks { private set; get; }
        public List<int> AllPeaksIdx { private set; get; }
        private List<int> curPeaksIdx; 
        public List<int> CurPeaksIdx
        {
            private set
            {
                if (curPeaksIdx != null)
                    curPeaksIdx.Clear();
                curPeaksIdx = value;
            } 
            get { return curPeaksIdx; }
        }
        private DataType curShowType;
        private double cutThreshold;
        public DataType CurShowType
        {
            set
            {
                curShowType = value;
                curPeaksIdx = retrievePeaks(cutThreshold, curShowType);
            }
            get { return curShowType; }
        }
        public double CutThreshold
        {
            set
            {
                cutThreshold = value;
                curPeaksIdx = retrievePeaks(cutThreshold, curShowType);
            }
            get { return cutThreshold; }
        }
        public int DecimalPlace { set; get; }
        public double YMax { private set; get; }
        public double XMin { private set; get; }
        public double XMax { private set; get; }
        public String StrSourceData { get; set; }

        public enum DataType
        {
            DT_Percentage,
            DT_AbsoluteValue,
        };

        public bool Good
        {
            get { return (XData != null && YData != null && YPercentageData != null); }
        }

        public bool Parse()
        {
            string[] splitStr = StrSourceData.Split(new char[]{'\r','\n'});
            string pattern = @"^(-?\d+)(\.\d+)?\t(-?\d+)(\.\d+)?$";
            var RetData = splitStr.Where(n => Regex.IsMatch(n, pattern));
            if (XData == null)
                XData = new List<double>();
            else
                XData.Clear();
            if (YData == null)
                YData = new List<double>();
            else
                YData.Clear();
            foreach (var strLine in RetData)
            {
                string[] tmp = strLine.Split('\t');
                XData.Add(Convert.ToDouble(tmp[0]));
                YData.Add(Convert.ToDouble(tmp[1]));
            }
            XMin = XData.Min();
            XMax = XData.Max();
            YMax = YData.Max();
            calPercentageData();
            calPeaks();
            return true;
        }

        public List<int> retrievePeaks(double threshold, DataType dt)
        {
            List<int> res = new List<int>();
            switch (dt)
            {
                case DataType.DT_Percentage:
                    if (YPercentageData != null)
                        res = (from idx in AllPeaksIdx where YPercentageData[idx] > threshold select idx).ToList();
                    break;
                case DataType.DT_AbsoluteValue:
                    if (YData != null)
                        res = (from idx in AllPeaksIdx where YData[idx] > threshold select idx).ToList();
                    break;
            }
            return res;
        }

        public bool isPeak(int idx)
        {
            return AllPeaksIdx.Any(i => i == idx);
        }

        private void calPercentageData()
        {
            if (YPercentageData == null)
                YPercentageData = new List<double>();
            else
                YPercentageData.Clear();
            foreach (var d in YData)
            {
                YPercentageData.Add(d*100/YMax);
            }
        }

        private void calPeaks()
        {
            if (AllPeaksIdx == null)
                AllPeaksIdx = new List<int>();
            else
                AllPeaksIdx.Clear();
            for (int i = 1; i < YData.Count - 1; i++)
            {
                if (YData[i] - YData[i - 1] > 0 && YData[i] - YData[i + 1] > 0)
                    AllPeaksIdx.Add(i);
            }
            try
            {
                curPeaksIdx = retrievePeaks(cutThreshold, curShowType);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
