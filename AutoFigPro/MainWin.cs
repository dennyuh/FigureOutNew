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
    public partial class MainWin : Form
    {
        private String strSingleFilename;
        private String strSingleTargetFilename;
        private String strSourceData;
        public  DataParse ParseInst;
        private InstalledFontCollection fonts;
        private ChartWin FigWin;
        private readonly string defaultProfilename = "DefaultParams.xml";
        private readonly string appPath = Directory.GetCurrentDirectory();
        private string lastValidXFontSize;
        private string lastValidYFontSize;
        private string lastValidCurveFontSize;
        private string lastValidCutOffPercentage;
        public MainWin()
        {
            InitializeComponent();
            try
            {
                FigWin = new ChartWin(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            ParseInst = new DataParse();
            fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                comboSingleXFont.Items.Add(family.Name);
                comboSingleYFont.Items.Add(family.Name);
                comboCurveTextFont.Items.Add(family.Name);
            }
            // Load default parameters from profile
            Directory.SetCurrentDirectory(appPath);
            setParametersFormProfile(defaultProfilename);
            lastValidXFontSize = textXFontSize.Text.Trim();
            lastValidYFontSize = textYFontSize.Text.Trim();
            lastValidCurveFontSize = textCurveTextFontSize.Text.Trim();
            lastValidCutOffPercentage = textCutOff.Text.Trim();
        }

        private void setParametersFormProfile(string filename)
        {
            XmlReader rdr = XmlReader.Create(filename);
            while (rdr.Read())
            {
                if (rdr.MoveToContent() == XmlNodeType.Element && rdr.AttributeCount == 2)
                {
                    string controlName = rdr.GetAttribute(0);
                    string controlValue = rdr.GetAttribute(1);
                    if (rdr.Name == "ControlText")
                        Controls.Find(controlName, true)[0].Text = controlValue;
                    else if (rdr.Name == "ControlCheck")
                        ((CheckBox) Controls.Find(controlName, true)[0]).Checked = controlValue == "true" ? true : false;
                }
            }
            rdr.Close();
        }

        private void saveProfile(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            XmlWriter writer = XmlWriter.Create(filename, settings);
            writer.WriteStartDocument();
                //axis parameters
            writer.WriteStartElement("Parameters");
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textSingleXLabel.Name);
                    writer.WriteAttributeString("Value", textSingleXLabel.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textSingleYLabel.Name);
                    writer.WriteAttributeString("Value", textSingleYLabel.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboSingleXFont.Name);
                    writer.WriteAttributeString("Value", comboSingleYFont.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboSingleYFont.Name);
                    writer.WriteAttributeString("Value", comboSingleYFont.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkSingleXBold.Name);
                    writer.WriteAttributeString("Value", checkSingleXBold.Checked==true?"true":"false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkSingleYBold.Name);
                    writer.WriteAttributeString("Value", checkSingleYBold.Checked == true ? "true" : "false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkSingleXItalic.Name);
                    writer.WriteAttributeString("Value", checkSingleXItalic.Checked == true ? "true" : "false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkSingleYItalic.Name);
                    writer.WriteAttributeString("Value", checkSingleYItalic.Checked == true ? "true" : "false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textXFontSize.Name);
                    writer.WriteAttributeString("Value", textXFontSize.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textYFontSize.Name);
                    writer.WriteAttributeString("Value", textYFontSize.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboYValue.Name);
                    writer.WriteAttributeString("Value", comboYValue.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textXFontSize.Name);
                    writer.WriteAttributeString("Value", textXFontSize.Text);
                writer.WriteEndElement();
                //curve parameters
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboCurveTextFont.Name);
                    writer.WriteAttributeString("Value", comboCurveTextFont.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkCurveTextBold.Name);
                    writer.WriteAttributeString("Value", checkCurveTextBold.Checked == true ? "true" : "false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlCheck");
                    writer.WriteAttributeString("Name", checkCurveTextItalic.Name);
                    writer.WriteAttributeString("Value", checkCurveTextItalic.Checked == true ? "true" : "false");
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textCurveTextFontSize.Name);
                    writer.WriteAttributeString("Value", textCurveTextFontSize.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboCurveTextDir.Name);
                    writer.WriteAttributeString("Value", comboCurveTextDir.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", textCutOff.Name);
                    writer.WriteAttributeString("Value", textCutOff.Text);
                writer.WriteEndElement();
                writer.WriteStartElement("ControlText");
                    writer.WriteAttributeString("Name", comboAccuracy.Name);
                    writer.WriteAttributeString("Value", comboAccuracy.Text);
                writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void radioSingleFromClipboard_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                butSinglePaste.Enabled = true;
                butSingleSourceBrowse.Enabled = false;
                textSingleSourceFile.Enabled = false;
            }
        }

        private void radioSingleSourceFile_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked)
            {
                butSingleSourceBrowse.Enabled = true;
                textSingleSourceFile.Enabled = true;
                butSinglePaste.Enabled = false;
            }
        }

        private void butSingleSourceBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = appPath;
            openFileDlg.Filter = "text file (*.txt)|*.txt";
            openFileDlg.RestoreDirectory = true;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                strSingleFilename = openFileDlg.FileName;
                textSingleSourceFile.Text = strSingleFilename;
                StreamReader sr = new StreamReader(textSingleSourceFile.Text);
                ParseInst.StrSourceData = sr.ReadToEnd();
                ParseInst.Parse();
                clearFig();
                updateFigure();
                updateTextOnFigure();
            }
        }
        
        private void clearFig()
        {
            FigWin.chartCtrl.Series["MainData"].Points.Clear();
        }

        private void updateTextOnFigure()
        {
            if (ParseInst.AllPeaksIdx != null && ParseInst.CurPeaksIdx != null)
            {
                // clear all value labels
                foreach (int idx in ParseInst.AllPeaksIdx)
                {
                    FigWin.chartCtrl.Series["MainData"].Points[idx].Label = "";
                }
                // add value labels
                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalDigits = Convert.ToInt16(comboAccuracy.Text);
                foreach (int idx in ParseInst.CurPeaksIdx)
                {
                    FigWin.chartCtrl.Series["MainData"].Points[idx].Label = ParseInst.XData[idx].ToString("N", provider);
                }
            }
        }

        private void updateValueLabelFont()
        {
            try
            {
                FontStyle fs = checkCurveTextBold.Checked ? FontStyle.Bold : FontStyle.Regular;
                fs |= checkCurveTextItalic.Checked ? FontStyle.Italic : FontStyle.Regular;
                FigWin.chartCtrl.Series["MainData"].Font = new Font(comboCurveTextFont.Text.Trim(),
                                                                    (float)
                                                                    Convert.ToDecimal(textCurveTextFontSize.Text.Trim()),
                                                                    fs);
            }
            catch (FormatException e)
            {
                // do nothing;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        private void updateFigure()
        {
            if (comboYValue.Text == "Percentage" && ParseInst.Good)
            {
                FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
                FigWin.chartCtrl.Series["MainData"].Points.DataBindXY(ParseInst.XData, ParseInst.YPercentageData);
            }
            else if (comboYValue.Text == "Absolute value" && ParseInst.Good)
            {
                FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisY.Maximum = ParseInst.YMax;
                FigWin.chartCtrl.Series["MainData"].Points.DataBindXY(ParseInst.XData, ParseInst.YData);
            }
        }

        private void butSingleTargetBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog savFileDlg = new SaveFileDialog();
            savFileDlg.InitialDirectory = appPath;
            savFileDlg.Filter = "Adobe Illustrator file (*.ai)|*.ai|Encapsulated PostScript file (*.eps)|*.eps";
            savFileDlg.RestoreDirectory = true;
            if (savFileDlg.ShowDialog() == DialogResult.OK)
            {
                strSingleTargetFilename = savFileDlg.FileName;
                textSingleTargetFile.Text = strSingleTargetFilename;
            }
        }

        private void butSinglePaste_Click(object sender, EventArgs e)
        {
            IDataObject dataHandle = Clipboard.GetDataObject();
            if (dataHandle.GetDataPresent(DataFormats.Text))
            {
                strSourceData = (String) dataHandle.GetData(DataFormats.Text);
                ParseInst.StrSourceData = strSourceData;
                ParseInst.Parse();
                clearFig();
                updateFigure();
                updateTextOnFigure();
            }
        }

        private void butSingleSave_Click(object sender, EventArgs e)
        {
            if (textSingleTargetFile.Text != "")
            {
                try
                {
                    MessageBox.Show("File saved successfully!");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select target file");
            }
        }

        private void butSaveProfile_Click(object sender, EventArgs e)
        {
            SaveFileDialog savFileDlg = new SaveFileDialog();
            savFileDlg.InitialDirectory = appPath;
            savFileDlg.Filter = "XML file (*.xml)|*.xml";
            savFileDlg.RestoreDirectory = true;
            if (savFileDlg.ShowDialog() == DialogResult.OK)
            {
                saveProfile(savFileDlg.FileName);
            }
        }

        private void comboCurveTextDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboCurveTextDir.Text)
            {
                case "Top":
                    FigWin.chartCtrl.Series["MainData"]["LabelStyle"] = "Top";
                    FigWin.chartCtrl.Series["MainData"].LabelAngle = 0;
                    break;
                case "Left":
                    FigWin.chartCtrl.Series["MainData"]["LabelStyle"] = "Left";
                    FigWin.chartCtrl.Series["MainData"].LabelAngle = -90;
                    break;
                case "Right":
                    FigWin.chartCtrl.Series["MainData"]["LabelStyle"] = "Right";
                    FigWin.chartCtrl.Series["MainData"].LabelAngle = 90;
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void butView_Click(object sender, EventArgs e)
        {
            FigWin.Show();
        }

        private void butLoadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.InitialDirectory = appPath;
            openFileDlg.Filter = "XML file (*.xml)|*.xml";
            openFileDlg.RestoreDirectory = true;
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                setParametersFormProfile(openFileDlg.FileName);
            }
        }

        private bool isValidDecimal(string strIn)
        {
            return Regex.IsMatch(strIn, @"^(\d+)(\.\d+)?$");
        }

        private void textSingleXLabel_TextChanged(object sender, EventArgs e)
        {
            FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisX.Title = textSingleXLabel.Text.Trim();
        }

        private void comboSingleXFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            setXLabelFont();
        }

        private void checkSingleXBold_CheckedChanged(object sender, EventArgs e)
        {
            setXLabelFont();
        }

        private void checkSingleXItalic_CheckedChanged(object sender, EventArgs e)
        {
            setXLabelFont();
        }

        private void textXFontSize_TextChanged(object sender, EventArgs e)
        {
            if (isValidDecimal(textXFontSize.Text.Trim()))
                setXLabelFont();
        }

        private void setXLabelFont()
        {
            try
            {
                FontStyle fs = checkSingleXBold.Checked ? FontStyle.Bold : FontStyle.Regular;
                fs |= checkSingleXItalic.Checked ? FontStyle.Italic : FontStyle.Regular;
                FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font(comboSingleXFont.Text.Trim(),
                                                                                     (float)
                                                                                     Convert.ToDecimal(
                                                                                         textXFontSize.Text.Trim()), fs);
            }
            catch (FormatException e)
            {
                // do nothing;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void textXFontSize_Leave(object sender, EventArgs e)
        {
            if (isValidDecimal(textXFontSize.Text.Trim()))
            {
                lastValidXFontSize = textXFontSize.Text.Trim();
                setXLabelFont();
            }
            else
            {
                textXFontSize.Text = lastValidXFontSize;
                setXLabelFont();
            }
        }

        private void textXFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (isValidDecimal(textXFontSize.Text.Trim()))
                {
                    lastValidXFontSize = textXFontSize.Text.Trim();
                    setXLabelFont();
                }
                else
                {
                    textXFontSize.Text = lastValidXFontSize;
                    setXLabelFont();
                }
            }
        }

        private void setYLabelFont()
        {
            try
            {
                FontStyle fs = checkSingleYBold.Checked ? FontStyle.Bold : FontStyle.Regular;
                fs |= checkSingleYItalic.Checked ? FontStyle.Italic : FontStyle.Regular;
                FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font(comboSingleYFont.Text.Trim(),
                                                                                     (float)
                                                                                     Convert.ToDecimal(
                                                                                         textYFontSize.Text.Trim()), fs);
            }
            catch (FormatException e)
            {
                //do nothing;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void textYFontSize_TextChanged(object sender, EventArgs e)
        {
            if (isValidDecimal(textXFontSize.Text.Trim()))
                setYLabelFont();
        }

        private void textYFontSize_Leave(object sender, EventArgs e)
        {
            if (isValidDecimal(textYFontSize.Text.Trim()))
            {
                lastValidYFontSize = textYFontSize.Text.Trim();
                setYLabelFont();
            }
            else
            {
                textYFontSize.Text = lastValidYFontSize;
                setYLabelFont();
            }
        }

        private void textYFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (isValidDecimal(textYFontSize.Text.Trim()))
                {
                    lastValidYFontSize = textYFontSize.Text.Trim();
                    setYLabelFont();
                }
                else
                {
                    textYFontSize.Text = lastValidYFontSize;
                    setYLabelFont();
                }
            }
        }

        private void comboSingleYFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            setYLabelFont();
        }

        private void checkSingleYBold_CheckedChanged(object sender, EventArgs e)
        {
            setYLabelFont();
        }

        private void checkSingleYItalic_CheckedChanged(object sender, EventArgs e)
        {
            setYLabelFont();
        }

        private void comboCurveTextFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateValueLabelFont();
        }

        private void checkCurveTextBold_CheckedChanged(object sender, EventArgs e)
        {
            updateValueLabelFont();
        }

        private void checkCurveTextItalic_CheckedChanged(object sender, EventArgs e)
        {
            updateValueLabelFont();
        }

        private void textCurveTextFontSize_TextChanged(object sender, EventArgs e)
        {
            if (isValidDecimal(textCurveTextFontSize.Text.Trim()))
                updateValueLabelFont();
        }

        private void textCurveTextFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (isValidDecimal(textCurveTextFontSize.Text.Trim()))
                {
                    lastValidCurveFontSize = textCurveTextFontSize.Text.Trim();
                    updateValueLabelFont();
                }
                else
                {
                    textCurveTextFontSize.Text = lastValidCurveFontSize;
                    updateValueLabelFont();
                }
            }
        }

        private void textCurveTextFontSize_Leave(object sender, EventArgs e)
        {
            if (isValidDecimal(textCurveTextFontSize.Text.Trim()))
            {
                lastValidCurveFontSize = textCurveTextFontSize.Text.Trim();
                updateValueLabelFont();
            }
            else
            {
                textCurveTextFontSize.Text = lastValidCurveFontSize;
                updateValueLabelFont();
            }
        }

        private bool isValidPercentage(string strIn)
        {
            return (isValidDecimal(strIn) && Convert.ToDouble(strIn) >= 0 && Convert.ToDouble(strIn) <= 100);
        }

        private void textCutOff_TextChanged(object sender, EventArgs e)
        {
            if (isValidPercentage(textCutOff.Text.Trim()))
            {
                ParseInst.CutThreshold = (double) Convert.ToDecimal(textCutOff.Text.Trim());
                updateTextOnFigure();
            }
        }

        private void textCutOff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (isValidPercentage(textCutOff.Text.Trim()))
                {
                    lastValidCutOffPercentage = textCutOff.Text.Trim();
                    ParseInst.CutThreshold = (double)Convert.ToDecimal(textCutOff.Text.Trim());
                    updateTextOnFigure();
                }
                else
                {
                    textCutOff.Text = lastValidCutOffPercentage;
                    ParseInst.CutThreshold = (double)Convert.ToDecimal(textCutOff.Text.Trim());
                    updateTextOnFigure();
                }
            }
        }

        private void textCutOff_Leave(object sender, EventArgs e)
        {
            if (isValidPercentage(textCutOff.Text.Trim()))
            {
                lastValidCutOffPercentage = textCutOff.Text.Trim();
                ParseInst.CutThreshold = (double)Convert.ToDecimal(textCutOff.Text.Trim());
                updateTextOnFigure();
            }
            else
            {
                textCutOff.Text = lastValidCutOffPercentage;
                ParseInst.CutThreshold = (double)Convert.ToDecimal(textCutOff.Text.Trim());
                updateTextOnFigure();
            }
        }

        private void comboYValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseInst.CurShowType = comboYValue.Text == "Percentage"
                                        ? DataParse.DataType.DT_Percentage
                                        : DataParse.DataType.DT_AbsoluteValue;
            updateFigure();
        }

        private void comboAccuracy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParseInst.DecimalPlace = Convert.ToInt16(comboAccuracy.Text);
            updateTextOnFigure();
        }

        private void textSingleYLabel_TextChanged(object sender, EventArgs e)
        {
            FigWin.chartCtrl.ChartAreas["ChartArea1"].AxisY.Title = textSingleYLabel.Text.Trim();
        }

        private void radioSingleProcessing_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                groupSingle.Enabled = true;
                groupBatch.Enabled = false;
            }
        }

        private void radioBatchProcessing_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                groupSingle.Enabled = false;
                groupBatch.Enabled = true;
            }
        }

        private void checkSmartLocation_CheckedChanged(object sender, EventArgs e)
        {
            FigWin.chartCtrl.Series["MainData"].SmartLabelStyle.Enabled = checkSmartLocation.Checked ? true : false;
        }
    }
}
