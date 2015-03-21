using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CMVP
{
    public partial class SeriesControl : UserControl
    {
        Chart chart; //Pointer to the chart this control controls

        public SeriesControl(string name, Chart chart)
        {
            InitializeComponent();
            this.seriesTypeLabel.Text = name;
            this.chart = chart;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (SeriesControl sc in this.Parent.Controls)
            {
                if (sc.Location.Y > this.Location.Y)
                    sc.Location = new Point(0, sc.Location.Y - sc.Size.Height);
            }
            chart.Series.Remove(chart.Series.FindByName(this.ToString()));
            this.Parent.Controls.Remove(this);
        }

        private void seriesTypeLabel_Click(object sender, EventArgs e)
        {

        }

        public override string ToString()
        {
            return this.seriesTypeLabel.Text;
        }
    }
}
