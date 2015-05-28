using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CMVP
{
    public partial class PlatooningControlPanel : UserControl
    {
        public PlatooningControlPanel()
        {
            InitializeComponent();
        }

        private void carToFollowIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {      
                if (!carToFollowIDDropDown.Items.Contains(car.ID) &&
                   (((ComboBox)this.ParentForm.Controls.Find("carIDDropDown", true)[0]).SelectedItem.ToString() != Convert.ToString(car.ID)))
                    carToFollowIDDropDown.Items.Add(car.ID);
            }
        }

        private void updateStatusLabel()
        {
            while(!this.IsDisposed)
            {
                //try
                {
                    Car activeCar = Program.cars.Find(car => car.ID == Convert.ToInt32(((ComboBox)this.ParentForm.Controls.Find("trafficCarIDDropDown", true)[0]).SelectedItem.ToString()));
                }
                //catch(Exception e)
                {
                    
                }
            }


        }

        private void carToFollowIDDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            //activeCar = Program.cars.Find(car => car.ID == Convert.ToInt32(((ComboBox)this.ParentForm.Controls.Find("trafficCarIDDropDown", true)[0]).SelectedItem.ToString()));
        }

        public void startStatusLabelThread()
        {
            //Thread thread = new Thread(new ThreadStart(updateStatusLabel));
            //thread.Start();
        }


        public int getCarToFollowID()
        {
            return (int) carToFollowIDDropDown.SelectedItem;
        }

        public float distance { get { return (float)distanceNumeric.Value; } }
        public float kp { get { return (float)kpNumeric.Value; } }
        public float ki { get { return (float)kiNumeric.Value; } }
        public float kd { get { return (float)kdNumeric.Value; } }
        public int followedCarID { get { return Convert.ToInt32(carToFollowIDDropDown.SelectedItem); } }
        
    }
}
