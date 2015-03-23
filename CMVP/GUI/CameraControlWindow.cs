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
        private Timer updatePreviewTimer;
        private ImageProcessing imgProcess;
        public CameraControlWindow()
        {
            InitializeComponent();
            videoStreamPanel.BackColor=Color.SpringGreen;
            Program.videoStream.pushDestination(videoStreamPanel);
            videoStreamPanel.Size = Program.videoStream.getSize();
            this.AutoSize = true;
            this.imgProcess = (ImageProcessing)Program.imageProcess;
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
            imgProcess.drawCirkelsOnImg = checkBoxDrawCirkels.Checked;
        }

        private void checkBoxDrawCenters_CheckedChanged(object sender, EventArgs e)
        {
            imgProcess.drawCenterOnImg = checkBoxDrawCenters.Checked;
        }
        private void checkBoxDrawDirection_CheckedChanged(object sender, EventArgs e)
        {
            imgProcess.drawDirectionOnImg = checkBoxDrawDirection.Checked;

        }
        private void checkBoxDrawTrack_CheckedChanged(object sender, EventArgs e)
        {
            imgProcess.drawTrackOnImg = checkBoxDrawTrack.Checked;
        }

        private void checkBoxDrawId_CheckedChanged(object sender, EventArgs e)
        {
            imgProcess.drawCarIdOnImg = checkBoxDrawId.Checked;
        }

        private void checkBoxDrawWindows_CheckedChanged(object sender, EventArgs e)
        {
            imgProcess.drawWindowsOnImg = checkBoxDrawWindows.Checked;
        }




    }
}
