using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Math.Geometry;
using AForge.Math;
using AForge;


namespace CMVP
{
    public class Item
    {
        AForge.IntPoint position;
        int size;
        public Item(AForge.IntPoint position, int size)
        {
            this.position = position;
            this.size = size;
        }
        int getSize()
        {
            return size;
        }
        public void setPosition(AForge.IntPoint pos)
        {
            this.position = pos;
        }
        public virtual AForge.IntPoint getPosition()
        {
            return position;
        }

    }
}
