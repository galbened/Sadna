using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Driver
{
    public class Driver
    {
        private static IApplicationBridge getBridge()
        {
            BridgeProxy bridge = new BridgeProxy();

            bridge.SetRealBridge(new BridgeReal()); // add real bridge here
            return bridge;
        }

        public static int Main(string[] args)
        {
            IApplicationBridge bridge = getBridge();
            return 0;
        }
	

    }
}
