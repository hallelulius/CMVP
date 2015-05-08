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
using System.Diagnostics;

namespace CMVP
{
    
    public partial class mainGUI : Form
    {
        //private Thread thread;
        private Brain brain = new Brain();
        private List<Track> tracks = new List<Track>();
        private CameraControlWindow ccw;
        private PerformanceAnalyzerWindow paw;
        private int dataGridUpdateTime = 1000;
        //public event FormClosingEventHandler FormClosing;

        public mainGUI()
        {
            InitializeComponent();
            loadTracks();
            brain.start();            
            ccw = new CameraControlWindow();
            paw = new PerformanceAnalyzerWindow();
            brain.analyzer = paw;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval=1000;
            timer.Tick += new EventHandler(deltaTime);
            timer.Tick += new EventHandler(updateDataGrid);
            timer.Start();
        }

        private void deltaTime(object sender, EventArgs e)
        {
            deltaTimeLabel.Text = "Update frequency: " + 1.0/Program.imageProcess.getDeltaTime();
        }

        private void mainGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            brain.StopWorking();
            Program.com.stopCars();
            Thread.Sleep(100);
            //Environment.Exit(0);
        }
        private void loadTracks() // Searches for .txt files in the "Tracks" folder and adds them to the tracks menu.
        {
            string currentFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            
            try
            {
                string[] paths = Directory.GetFiles(currentFolder + @"\Tracks\", "*.txt");

                foreach (string path in paths)
                {
                    Track track = new Track(path);
                    tracksDropDown.Items.Add(track.name);
                    tracks.Add(track);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't locate the 'Tracks' folder. The program will not load tracks automatically. To fix this add the folder to the applications directory (" + currentFolder + ").");
                Debug.WriteLine(ex.Message);
            }
        }
        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            calibration.Enabled = false;
            Initiate.Enabled = false;
            brain.StartWorking();
            Console.WriteLine("Starting simulation...");
            
        }
        private void stopSimulationButton_Click(object sender, EventArgs e)
        {
            calibration.Enabled = true;
            Initiate.Enabled = true;
            Console.WriteLine("Stoping simulation...");
            try
            {
                foreach(Car car in Program.cars)
                {
                    Program.com.stopCar(car.ID);
                    car.getController().resetController();
                }
                brain.StopWorking();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void openCameraControlButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Opening camera control");
            //ccw.setPanelSize(Program.videoStream.getSize());
            ccw.Show();

        }

        private void controllerTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            controllerTypePanel.Controls.Clear();
            applyButton.Enabled = true;

            if (controllerTypeDropDown.SelectedIndex != -1)
            {
                if (controllerTypeDropDown.SelectedItem.ToString() == "PID")
                    controllerTypePanel.Controls.Add(new PIDControlPanel());

                if (controllerTypeDropDown.SelectedItem.ToString() == "Manual Keyboard")
                    controllerTypePanel.Controls.Add(new KeyboardControlPanel());
            }
        }

        private void importTrackButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to add the track to your 'Tracks' folder? If you press 'Yes', it will load automatically the next time you start the application.",
                    "Importing track",
                    MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    // Open the file:
                    string file = openFileDialog.FileName;

                    // Create a track from the file:
                    Track track = new Track(file);
                    tracksDropDown.Items.Add(track.name);
                    tracks.Add(track);

                    // Copy the file into the 'Tracks' folder:
                    string currentFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    try
                    {
                        File.Copy(file, currentFolder + @"\Tracks\" + Path.GetFileName(file));
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Directory.CreateDirectory(currentFolder + @"\Tracks\");
                        File.Copy(file, currentFolder + @"\Tracks\" + Path.GetFileName(file));
                        Debug.WriteLine(ex.Message);
                    }

                }
                if (result == DialogResult.No)
                {
                    Track track = new Track(openFileDialog.FileName);
                    tracksDropDown.Items.Add(track.name);
                    tracks.Add(track);
                }
            }
        }

        private void trackCarIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {
                if (!carIDDropDown.Items.Contains(car.ID))
                    carIDDropDown.Items.Add(car.ID);
            }
        }

        private void openPerformanceAnalyzerButton_Click(object sender, EventArgs e)
        {
            paw.Show();
            }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (carIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)carIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                // Set track:
                if(tracksDropDown.SelectedIndex != -1)
                    tempCar.getControlStrategy().setTrack(tracks.Find(t => t.name == tracksDropDown.SelectedItem.ToString()));

                // Set controller
                if (controllerTypeDropDown.SelectedItem.ToString() == "PID")
                {
                    PIDController controller = new PIDController(tempCar);

                    foreach (Control ctrl in controllerTypePanel.Controls)
                    {
                      controller.KpSteer = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kpSteerNumeric", true)[0])).Value);
                      controller.KpThrottle = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kpThrottleNumeric", true)[0])).Value);
                      controller.KiSteer = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kiSteerNumeric", true)[0])).Value);
                      controller.KiThrottle = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kiThrottleNumeric", true)[0])).Value);
                      controller.KdSteer = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kdSteerNumeric", true)[0])).Value);
                      controller.KdThrottle = (float)Convert.ToDouble(((NumericUpDown)(ctrl.Controls.Find("kdThrottleNumeric", true)[0])).Value);
                      
                    }
                    tempCar.setController(controller);
                    updateControllerParametersGUI(tempCar);

                }

                if (controllerTypeDropDown.SelectedItem.ToString() == "Manual keyboard")
                {
                    tempCar.setController(new KeyboardController(tempCar));
                }

                // Set control strategy
                if (controlStrategyDropDown.SelectedItem.ToString() == "Follow track")
        {
                    ControlStrategies.JustFollow jf = new ControlStrategies.JustFollow(tempCar);
                    jf.setTrack(tempCar.getControlStrategy().getTrack());
                    tempCar.setControlStrategy(jf);
        }
                if (controlStrategyDropDown.SelectedItem.ToString() == "Stand still")
            {
                    ControlStrategies.StandStill ss = new ControlStrategies.StandStill(tempCar);
                    ss.setTrack(tempCar.getControlStrategy().getTrack());
                    tempCar.setControlStrategy(ss);
        }
                if (controlStrategyDropDown.SelectedItem.ToString() == "Overtaking")
            {
                    ControlStrategies.Overtaking ot = new ControlStrategies.Overtaking(tempCar);
                    ot.setTrack(tempCar.getControlStrategy().getTrack());
                    tempCar.setControlStrategy(ot);
        }

                // Set max speed
                tempCar.setMaxSpeed((float)maxSpeedNumeric.Value);
            }
        }

        private void updateControllerParametersGUI(Car car)
        {
            foreach (Control ctrl in controllerTypePanel.Controls)
            {
                if (car.getController().getName() == "PID")
                {
                    ((NumericUpDown)(ctrl.Controls.Find("kpSteerNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KpSteer);
                    ((NumericUpDown)(ctrl.Controls.Find("kpThrottleNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KpThrottle);
                    ((NumericUpDown)(ctrl.Controls.Find("kiSteerNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KiSteer);
                    ((NumericUpDown)(ctrl.Controls.Find("kiThrottleNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KiThrottle);
                    ((NumericUpDown)(ctrl.Controls.Find("kdSteerNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KdSteer);
                    ((NumericUpDown)(ctrl.Controls.Find("kdThrottleNumeric", true)[0])).Value = Convert.ToDecimal(((PIDController)car.getController()).KdThrottle);
                }
            }
        }

        private void ptgrey_Click(object sender, EventArgs e)
        {
            //GUI.PTGreyForm ptgf = new GUI.PTGreyForm();
            //ptgf.Show();
        }

        private void Initiate_Click(object sender, EventArgs e)
        {
            brain.StopWorking();
            Program.imageProcess.initiate();
            startSimulationButton.Enabled = true;
            calibration.Enabled = true;
            dataGridView.Rows.Clear();
            if (Program.cars.Count > 0)
                dataGridView.Rows.Add(Program.cars.Count);
        }

        private void controlStrategyControlStrategyDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            controlStrategyPanel.Controls.Clear();
            applyButton.Enabled = true;

            if (controlStrategyDropDown.SelectedIndex != -1)
            {
                if (controlStrategyDropDown.SelectedItem.ToString() == "Platooning")
                    controlStrategyPanel.Controls.Add(new PlatooningControlPanel());
            }
        }

        private void trafficMaxSpeedNumeric_ValueChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;
        }

        private void updateDataGrid(object sender, EventArgs e)
        {
                //Console.WriteLine("Updating data grid...");
                foreach(DataGridViewRow row in dataGridView.Rows)
                {
                    Car car = Program.cars.ElementAt(row.Index);

                    row.Cells[0].Value = car.ID;
                    row.Cells[1].Value = car.getPosition().X;
                    row.Cells[2].Value = car.getPosition().Y;
                    row.Cells[3].Value = car.getSpeed();
                    row.Cells[4].Value = car.getAngle();
                    row.Cells[5].Value = car.getController().getSteer();
                    row.Cells[6].Value = car.getController().getThrottle();
                }
        }

        private void dataGridTimeNumeric_ValueChanged(object sender, EventArgs e)
        {
            dataGridUpdateTime = Convert.ToInt32(dataGridTimeNumeric.Value);
        }

        private void calibration_Click(object sender, EventArgs e)
        {
            foreach (Car c in Program.cars)
            {
                Program.com.calibrationMode(c.ID);
                MessageBox.Show("Calibrate the controller for car: " + c.ID + ".\n Read the instructions in the manual chapter 6,4. \n Remember to always run the platform with external power source.");
            }
        }

        private void carIDDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {
                if (!carIDDropDown.Items.Contains(car.ID))
                    carIDDropDown.Items.Add(car.ID);
            }

            applyButton.Enabled = true;
        }

        private void maxSpeedUpdate_Click(object sender, EventArgs e)
        {
            if (carIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)carIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                tempCar.setMaxSpeed((float)maxSpeedNumeric.Value);
            }
        }

        private void tracksDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyButton.Enabled = true;
        }
    }
}