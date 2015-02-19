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


        }
        public void updatePreview(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
