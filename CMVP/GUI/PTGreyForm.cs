using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CMVP.GUI
{
    public partial class PTGreyForm : Form
    {
        private static PictureBox  video;

        public PTGreyForm()
        {
            InitializeComponent();
            video = pictureBox1;
        }

        private void PTGreyForm_Load(object sender, EventArgs e)
        {

        }

        private void raw_CheckedChanged(object sender, EventArgs e)
        {

            if (raw.Checked == true)
            {
                Program.ptg.start();
                pictureBox1.Image = Program.ptg.getImage();
                pictureBox1.Invalidate();
            }
            else
            {
                Program.ptg.stop();
            }
        }
    }
}
