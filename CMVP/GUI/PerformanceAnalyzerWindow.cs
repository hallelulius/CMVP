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
    public partial class PerformanceAnalyzerWindow : Form
    {
        private float uppdateTime = 1; // Time between uppdates.
        private List<DataBinding> _dataBindings;

        public PerformanceAnalyzerWindow()
        {
            InitializeComponent();
        }

        public void addDataBinding()
        {

        }

        private class DataBinding
        {
        }
    }
}
