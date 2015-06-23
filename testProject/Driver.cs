using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Driver;

namespace testProject
{
    class Driver
    {
        public static IApplicationBridge GetBridge()
        {
            BridgeProxy bridge = new BridgeProxy();

            bridge.SetRealBridge(BridgeReal.GetInstance()); // add real bridge here
            return bridge;
        }
    }
}
