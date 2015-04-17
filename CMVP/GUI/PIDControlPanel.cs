using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CMVP
{
    public partial class PIDControlPanel : UserControl
    {
        public PIDControlPanel()
        {
            InitializeComponent();
        }

        private void numericValueChanged(object sender, EventArgs e)
        {
            ((Button)this.ParentForm.Controls.Find("controllerApplyButton", true)[0]).Enabled = true;
        }

        private void PIDControlPanel_Load(object sender, EventArgs e)
        {

        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;
            saveDialog.ShowDialog();
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveDialog.FileName);

                file.WriteLine("PID-settings from CVMP");
                file.WriteLine(kpSteerNumeric.Value);
                file.WriteLine(kiSteerNumeric.Value);
                file.WriteLine(kdSteerNumeric.Value);
                file.WriteLine(tiSteerNumeric.Value);
                file.WriteLine(kpThrottleNumeric.Value);
                file.WriteLine(kiThrottleNumeric.Value);
                file.WriteLine(kdThrottleNumeric.Value);
                file.WriteLine(tiThrottleNumeric.Value);
                file.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void loadSettings_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            StreamReader sReader;
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;

            openDialog.ShowDialog();
            try
            {
                if ((stream = openDialog.OpenFile()) != null)
                {
                    using (stream)
                    {
                        // Insert code to read the stream here.
                        sReader = new StreamReader(stream);
                        sReader.ReadLine();
                        kpThrottleNumeric.Value = decimal.Parse(sReader.ReadLine());
                        kiSteerNumeric.Value = decimal.Parse(sReader.ReadLine());
                        kdSteerNumeric.Value = decimal.Parse(sReader.ReadLine());
                        tiSteerNumeric.Value = decimal.Parse(sReader.ReadLine());
                        kpThrottleNumeric.Value = decimal.Parse(sReader.ReadLine());
                        kiThrottleNumeric.Value = decimal.Parse(sReader.ReadLine());
                        kdThrottleNumeric.Value = decimal.Parse(sReader.ReadLine());
                        tiThrottleNumeric.Value = decimal.Parse(sReader.ReadLine());
                        sReader.Close();
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
    }
}
