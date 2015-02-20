using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMVP
{
    
    public partial class mainGUI : Form
    {
        private static Timer updatePreview;
        public mainGUI()
        {
            InitializeComponent();
            updatePreview = new Timer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Start simulation");
        }

        private void stopSimulationButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Stop simulation");
        }


        private void controllerTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openCameraControlButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Stop simulation");
            CameraControlWindow ccw = new CameraControlWindow();
            ccw.Show();
        }
    }
}