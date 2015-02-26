using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CMVP
{
    class Brain
    {
        
        private List<Car> cars;   
        public void run()
        {
            cars = Program.cars;
            while (true)
            {
                foreach(Car car in cars)
                {
                    car.updateState();
                }
                foreach (Car car in cars)
                {
                    car.getController().updateController();
                }
                foreach (Car car in cars)
                {
                    car.send();
                }
                Thread.Sleep(3);
            }
        }
    }
}
