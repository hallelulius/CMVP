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
    
    public partial class CameraControlWindow : Form
    {
        private ImageProcessing imgProcess;
        public CameraControlWindow()
        {
            InitializeComponent();
            videoStreamPanel.BackColor=Color.SpringGreen;
            Program.videoStream.pushDestination(videoStreamPanel);
            videoStreamPanel.Size = Program.videoStream.getSize();
            this.AutoSize = true;
            this.imgProcess = (ImageProcessing)Program.imageProcess;

            processedVideoRadioButton.Select();
            checkBoxDrawTrack.Checked = true;
            this.checkBoxDrawTrack_CheckedChanged_1(this, new EventArgs());
            checkBoxDrawWindows.Checked = true;
            this.checkBoxDrawWindows_CheckedChanged(this, new EventArgs());
            checkBoxDrawRefHeading.Checked = true;
            this.checkBoxDrawRefHeading_CheckedChanged(this, new EventArgs());
        }
        private void updatePreview(object sender, EventArgs e)
        {
            
            videoStreamPanel.BackgroundImage =new Bitmap(Program.videoStream.getImage(),videoStreamPanel.Width,videoStreamPanel.Height);
            
        }
        private void CameraControlWindow_Load(object sender, EventArgs e)
        {
           
        }

        private void videoStreamPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void processedVideoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(processedVideoRadioButton.Checked)
            {
                Console.WriteLine("Showing Processed Image");
                Program.videoStream.removeDestination(videoStreamPanel);
                Program.imageProcess.pushDestination(videoStreamPanel);
            }
            else
            {
                Console.WriteLine("Showing Raw Image");
                Program.imageProcess.removeDestination(videoStreamPanel);
                Program.videoStream.pushDestination(videoStreamPanel);
            }

        }

        private void checkBoxDrawCirkels_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawCirkels.Checked");
            imgProcess.drawCirkelsOnImg = checkBoxDrawCirkels.Checked;
        }

        private void checkBoxDrawCenters_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawCenters.Checked");
            imgProcess.drawCenterOnImg = checkBoxDrawCenters.Checked;
        }
        private void checkBoxDrawDirection_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawDirection.Checked");
            imgProcess.drawDirectionOnImg = checkBoxDrawDirection.Checked;

        }

        private void checkBoxDrawId_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawId.Checked");
            imgProcess.drawCarIdOnImg = checkBoxDrawId.Checked;
        }

        private void checkBoxDrawWindows_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawWindows.Checked");
            imgProcess.drawWindowsOnImg = checkBoxDrawWindows.Checked;
        }

        private void checkBoxDrawRefHeading_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawRefHeading.Checked");
            imgProcess.drawRefHeadingOnImg = checkBoxDrawRefHeading.Checked;
        }

        private void checkBoxDrawTrack_CheckedChanged_1(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawTrack.Checked");
            imgProcess.drawTrackOnImg = checkBoxDrawTrack.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.videoStream.showCameraSettings();
        }

    }
}
