using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CMVP
{
    interface VideoStream
    {
        Bitmap getImage();
        void start();
        void stop();
        void pushDestination(Panel panel);
        void removeDestination(Panel panel);
        Size getSize();
        float getTime();
    }

}
