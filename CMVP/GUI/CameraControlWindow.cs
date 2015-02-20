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
        public CameraControlWindow()
        {
            InitializeComponent();
            videoStreamPanel.BackColor=Color.SpringGreen;
            Program.videoStream.pushDestination(videoStreamPanel);
            videoStreamPanel.Size = Program.videoStream.getSize();
            this.AutoSize = true;
            
           

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
    }
}
