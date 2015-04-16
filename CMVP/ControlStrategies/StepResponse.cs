using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP.ControlStrategies
{
    class StepResponse:ControlStrategy
    {

        public StepResponse(Car car) : base(car,null, "Step response")
        {
            
        }
    }
}
