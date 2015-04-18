
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing; // Anvnänds då man skapar en egen bil i Program
using System.Runtime.InteropServices;

namespace CMVP
{
    class Program
    {
        //Global variables
        public static Communication com = new Communication();
        public static List<Car> cars = new List<Car>();
        public static List<Item> obstacle = new List<Item>();

        public static PTGreyCamera videoStream;
        public static ImageProcessing imageProcess;

        [STAThread]
        public static void Main()
        {
            //cars.Add(new Car(1, new AForge.IntPoint(0, 0), new AForge.Point(1, 0))); // endast för att testa en imaginär bil 
            //cars.Add(new Car(2, new AForge.IntPoint(0, 0), new AForge.Point(1, 0))); // endast för att testa en imaginär bil 
            comDebug cd = new comDebug();
            cd.Show();
            Application.Run(cd);
            /*
            mainGUI mainFrame = new mainGUI();
            videoStream = new PTGreyCamera();
            videoStream.start();
            System.Threading.Thread.Sleep(1000);
            imageProcess = new ImageProcessing(videoStream, cars);
            Application.Run(mainFrame);
            */
        }

    }

    //Debugging communication 
    public class comDebug : Form
    {
        Form f = new Form();
        static TextBox tb = new TextBox();
        static Label l = new Label();
        Button b = new Button();

        public comDebug()
            : base()
        {

            b.Text = "Send value";
            b.Click += new System.EventHandler(b_Click);
            tb.Location = new Point(100, 100);
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            b.Location = new Point(100, 150);
            l.Location = new Point(100, 200);
            this.Controls.Add(tb);
            this.Controls.Add(b);
            this.Controls.Add(l);
        }

        private void b_Click(object sender, System.EventArgs e)
        {
            Program.com.updateThrottle(1, float.Parse(tb.Text));
            l.Text = Program.com.lastValues.First().ToString();
        }
            

        static void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Program.com.updateThrottle(1, float.Parse(tb.Text));
                l.Text = Program.com.lastValues.First().ToString();
            }
        }
    }
}
