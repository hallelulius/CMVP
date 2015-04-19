using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP.Controllers
{
    class NoFeedback:Controller
    {
        private const int DATA_HISTORY_LENGTH = 5;
        //remove this just a test
        public NoFeedback(Car car)
            : base(car)
        {
            // Set controler name:
            controllerName = "No Feedback";
        }

        public override void updateController()
        {

            outThrottle = refSpeed;
            outSteer = refHeading;            
            outThrottle = capThrottleOutput(outThrottle);
            outSteer = capSteerOutput(outSteer);
        }

    }
}
