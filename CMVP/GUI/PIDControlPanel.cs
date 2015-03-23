using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMVP
{
    public partial class PIDControlPanel : UserControl
    {
        public PIDControlPanel()
        {
            InitializeComponent();
        }

        private void numericValueChanged(object sender, EventArgs e)
        {
            ((Button)this.ParentForm.Controls.Find("controllerApplyButton", true)[0]).Enabled = true;
        }
    }
}
