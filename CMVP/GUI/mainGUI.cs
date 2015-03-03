using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CMVP
{
    
    public partial class mainGUI : Form
    {
        private Thread thread;
        private Brain brain;
        public mainGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Start simulation");

            brain = new Brain();
            thread = new Thread(new ThreadStart(brain.run));
            thread.Start();

        }

        private void stopSimulationButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Stop simulation");
            thread.Abort();
            
        }

        private void openCameraControlButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Opening camera control");
            CameraControlWindow ccw = new CameraControlWindow();
            ccw.Show();
        }

        private void controllerTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            controllerTypePanel.Controls.Clear();

            if (controllerTypeDropDown.SelectedItem.ToString() == "P")
                controllerTypePanel.Controls.Add(new PControlPanel());

            if (controllerTypeDropDown.SelectedItem.ToString() == "PI")
                controllerTypePanel.Controls.Add(new PIControlPanel());

            if (controllerTypeDropDown.SelectedItem.ToString() == "PID")
                controllerTypePanel.Controls.Add(new PIDControlPanel());

            if (controllerTypeDropDown.SelectedItem.ToString() == "Manual Keyboard")
                controllerTypePanel.Controls.Add(new KeyboardControlPanel());
        }

        private void importTrackButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TrackImporter ti = new TrackImporter(openFileDialog.FileName);
            }
        }

        private void ptgrey_Click(object sender, EventArgs e)
        {
            GUI.PTGreyForm ptgf = new GUI.PTGreyForm();
            ptgf.Show();
        }
    }
}