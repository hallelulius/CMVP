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
    
    public partial class mainGUI : Form
    {
        private static Timer updatePreview;;
        public mainGUI()
        {
            InitializeComponent();
            updatePreview = new Timer();
            updatePreview.
            updatePreview.Interval(20);
        }
    }
}
