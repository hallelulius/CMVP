﻿using System;
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
        private static Timer updatePreview;
        public mainGUI()
        {
            InitializeComponent();
            updatePreview = new Timer();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}