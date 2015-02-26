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
                Track ti = new Track(openFileDialog.FileName);
                tracksDropDown.Items.Add(ti.name);
            }
        }

        private void trafficControlBasePanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void controllerCarIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {
                if (!controllerCarIDDropDown.Items.Contains(car.ID))
                    controllerCarIDDropDown.Items.Add(car.ID);
            }
        }

        private void trafficCarIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {
                if (!trafficCarIDDropDown.Items.Contains(car.ID))
                    trafficCarIDDropDown.Items.Add(car.ID);
            }
        }

        private void trafficApplyButton_Click(object sender, EventArgs e)
        {
            if (controllerCarIDDropDown.SelectedIndex != -1)
            {
                int tempID = (int)trafficCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                if (controlStrategyControlStrategyDropDown.SelectedItem.ToString() == "Follow track")
                    tempCar.setControlStrategy(new ControlStrategies.JustFollow());
                if (controlStrategyControlStrategyDropDown.SelectedItem.ToString() == "Stand still")
                    tempCar.setControlStrategy(new ControlStrategies.StandStill());
            }
        }

        private void trafficCancelButton_Click(object sender, EventArgs e)
        {
            trafficCarIDDropDown.SelectedIndex = -1;
            controlStrategyControlStrategyDropDown.SelectedIndex = -1;
        }

        private void trackCarIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {
                if (!trackCarIDDropDown.Items.Contains(car.ID))
                    trackCarIDDropDown.Items.Add(car.ID);
            }
        }
    }
}