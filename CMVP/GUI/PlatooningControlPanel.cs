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
        Car activeCar;

        public PlatooningControlPanel()
        {
            InitializeComponent();
        }

        private void carToFollowIDDropDown_DropDown(object sender, EventArgs e)
        {
            foreach (Car car in Program.cars)
            {      
                if (!carToFollowIDDropDown.Items.Contains(car.ID) &&
                   (((ComboBox)this.ParentForm.Controls.Find("trafficCarIDDropDown", true)[0]).SelectedItem.ToString() != Convert.ToString(car.ID)))
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
                    if (((ControlStrategies.Platooning)activeCar.getControlStrategy()).isFollowingLeader)
                        statusLabel.Text = "following";
                    else
                        statusLabel.Text = "searching";
                }
                //catch(Exception e)
                {
                    
                }
            }


        }

        private void carToFollowIDDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            activeCar = Program.cars.Find(car => car.ID == Convert.ToInt32(((ComboBox)this.ParentForm.Controls.Find("trafficCarIDDropDown", true)[0]).SelectedItem.ToString()));
        }

        public void startStatusLabelThread()
        {
            //Thread thread = new Thread(new ThreadStart(updateStatusLabel));
            //thread.Start();
        }

        public int getCarToFollowID()
        {
            return Convert.ToInt32(carToFollowIDDropDown.SelectedItem.ToString());
        }
    }
}
