using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AutoFigPro
{
    public partial class MainWin
    {
        public int getDecimalPlaces()
        {
            return System.Convert.ToInt16(comboAccuracy.Text);
        }
    }
}
