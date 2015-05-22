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
        private ImageProcessingGraphics ipd;
        public CameraControlWindow()
        {
            InitializeComponent();
            imgProcess = Program.imageProcess;
            ipd = Program.ipd;
            ipd.setPanel(videoStreamPanel);
            initSettings();
            videoStreamPanel.BackColor=Color.SpringGreen;
        }

        
        private void initSettings()
        {
            this.FormClosing += new FormClosingEventHandler(CCWFormClosing);
            threshold_ScrollBar.Value = imgProcess.getThreshold();
            scrollbar_label.Text = "Threshold: " + imgProcess.getThreshold();
            checkBoxDrawTrack.Checked = true;
            this.checkBoxDrawTrack_CheckedChanged_1(this, new EventArgs());
            checkBoxDrawWindows.Checked = true;
            this.checkBoxDrawWindows_CheckedChanged(this, new EventArgs());
            checkBoxDrawRefHeading.Checked = true;
            this.checkBoxDrawRefHeading_CheckedChanged(this, new EventArgs());
            checkBoxDrawTails.Checked = true;
            this.checkBoxDrawTails_CheckedChanged(this, new EventArgs());

        }
        private void CCWFormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }
        private void CameraControlWindow_Load(object sender, EventArgs e)
        {
            videoStreamPanel.Size = Program.videoStream.getSize();
        }

        private void videoStreamPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxDrawCirkels_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawCirkels.Checked");
            ipd.drawCirclesOnImg = checkBoxDrawCirkels.Checked;
        }

        private void checkBoxDrawCenters_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawCenters.Checked");
            ipd.drawCenterOnImg = checkBoxDrawCenters.Checked;
        }
        private void checkBoxDrawDirection_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawDirection.Checked");
            ipd.drawDirectionOnImg = checkBoxDrawDirection.Checked;

        }

        private void checkBoxDrawId_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawId.Checked");
            ipd.drawCarIdOnImg = checkBoxDrawId.Checked;
        }

        private void checkBoxDrawWindows_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawWindows.Checked");
            ipd.drawWindowsOnImg = checkBoxDrawWindows.Checked;
        }

        private void checkBoxDrawRefHeading_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawRefHeading.Checked");
            ipd.drawRefHeadingOnImg = checkBoxDrawRefHeading.Checked;
        }

        private void checkBoxDrawTrack_CheckedChanged_1(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawTrack.Checked");
            ipd.drawTrackOnImg = checkBoxDrawTrack.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.videoStream.showCameraSettings();
        }

        private void checkBoxDrawTails_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("checkBoxDrawTails.Checked");
            ipd.drawTailsOnImg = checkBoxDrawTails.Checked;
        }

        private void threshold_ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            imgProcess.setThreshold((byte) threshold_ScrollBar.Value);
            scrollbar_label.Text = "Threshold: " + imgProcess.getThreshold();
        }

        
    }
}
