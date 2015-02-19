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
        private static Timer updatePreviewTimer;
        //private static VideoStream videoStream;
        // static CameraController videoStream;
        private static Camera VideoStream;
        public mainGUI()
        {
            InitializeComponent();
            VideoStream = new Camera();
            VideoStream.startCamera();
            updatePreviewTimer = new Timer();
            updatePreviewTimer.Interval= 100;
            updatePreviewTimer.Tick += new EventHandler(this.updatePreview);
            updatePreviewTimer.Start();
            panel1.Show();

            Bitmap darkImage = new Bitmap(panel1.Width, panel1.Height);
            Graphics g = Graphics.FromImage(darkImage);
            g.FillRectangle(new SolidBrush(Color.FromArgb(0,0,0)), 0, 0, darkImage.Width, darkImage.Height);


            panel1.BackgroundImage = new Bitmap(darkImage);
        }
        public void updatePreview(object sender, EventArgs e)
        {
            Bitmap img = VideoStream.getImage();
            panel1.BackgroundImage = new Bitmap(img, panel1.Width, panel1.Height);
            

        }
    }
}
