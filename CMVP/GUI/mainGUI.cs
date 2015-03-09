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
        private Thread thread;
        private List<Track> tracks = new List<Track>();

        public mainGUI()
        {
            InitializeComponent();
            loadTracks();
            thread = new Thread(new ThreadStart(brain.run));
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
            catch (Exception exception)
            {
                MessageBox.Show("Couldn't locate the 'Tracks' folder. The program will not load tracks automatically. To fix this add the folder to the applications directory (" + currentFolder + ").");
            }
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            //System.Console.WriteLine("Start simulation");

            //brain = new Brain();
            //thread = new Thread(new ThreadStart(brain.run));
            //thread.Start();
            
            //Added this so that there isnt a new brain created whenever the "Start simulation" button is pressed:
            switch (thread.ThreadState)
            {
                case System.Threading.ThreadState.Unstarted:
                    thread.Start();
                    Console.WriteLine("Starting simulation...");
                    break;

                case System.Threading.ThreadState.Suspended:
                    thread.Resume();
                    Console.WriteLine("Resuming simulation...");
                    break;

                default:
                    Console.WriteLine("Simulation already running...");
                    break;
            }
        }

        private void stopSimulationButton_Click(object sender, EventArgs e)
        {
            //System.Console.WriteLine("Stop simulation");
            //thread.Abort();
            Console.WriteLine("Stoping simulation");
            try
            {
                thread.Suspend();
            }
            catch (Exception exception)
            {

            }
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
                    catch (DirectoryNotFoundException exception)
                    {
                        Directory.CreateDirectory(currentFolder + @"\Tracks\");
                        File.Copy(file, currentFolder + @"\Tracks\" + Path.GetFileName(file));
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
            if (trafficCarIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)trafficCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                if (controlStrategyControlStrategyDropDown.SelectedItem.ToString() == "Follow track")
                {
                    ControlStrategies.JustFollow jf = new ControlStrategies.JustFollow();
                    jf.setTrack(tempCar.getControlStrategy().getTrack());
                    tempCar.setControlStrategy(jf);
                }
                if (controlStrategyControlStrategyDropDown.SelectedItem.ToString() == "Stand still")
                {
                    ControlStrategies.StandStill ss = new ControlStrategies.StandStill(tempCar);
                    ss.setTrack(tempCar.getControlStrategy().getTrack());
                    tempCar.setControlStrategy(ss);
                }
            }
        }

        private void trafficCancelButton_Click(object sender, EventArgs e)
        {
            trafficCarIDDropDown.SelectedIndex = -1; // -1 means nothing is selected
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

        private void controllerApplyButton_Click(object sender, EventArgs e)
        {
            if (controllerCarIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)controllerCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                if (controllerTypeDropDown.SelectedItem.ToString() == "PID")
                {
                    tempCar.setController(new PIController());
                }

                if (controllerTypeDropDown.SelectedItem.ToString() == "Manual keyboard")
                {
                    tempCar.setController(new KeyboardController());
                }
            }
        }

        private void controllerCancelButton_Click(object sender, EventArgs e)
        {
            controllerCarIDDropDown.SelectedIndex = -1; // -1 means nothing is selected
            controllerTypeDropDown.SelectedIndex = -1;
            controllerTypePanel.Controls.Clear();
        }

        private void trackCancelButton_Click(object sender, EventArgs e)
        {
            trackCarIDDropDown.SelectedIndex = -1; // -1 means nothing is selected
            tracksDropDown.SelectedIndex = -1;
        }

        private void openPerformanceAnalyzerButton_Click(object sender, EventArgs e)
        {
            PerformanceAnalyzerWindow paw = new PerformanceAnalyzerWindow();
            brain.analyzer = paw;
            paw.Show();
        }

        private void trackApplyButton_Click(object sender, EventArgs e)
        {
            if (trackCarIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)trackCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                if(tracksDropDown.SelectedIndex != -1)
                    tempCar.getControlStrategy().setTrack(tracks.Find(t => t.name == tracksDropDown.SelectedItem.ToString()));
            }
        }

        private void trafficCarIDDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trafficCarIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)trafficCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                controlStrategyControlStrategyDropDown.SelectedItem = tempCar.getControlStrategy().getStrategyName();
            }
        }

        private void controllerCarIDDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controllerCarIDDropDown.SelectedIndex != -1) // -1 means nothing is selected
            {
                int tempID = (int)controllerCarIDDropDown.SelectedItem;
                Car tempCar = Program.cars.Find(car => car.ID == tempID);

                controllerTypeDropDown.SelectedItem = tempCar.getController().getName();
            }
        }

        private void ptgrey_Click(object sender, EventArgs e)
        {
            GUI.PTGreyForm ptgf = new GUI.PTGreyForm();
            ptgf.Show();
        }
    }
}