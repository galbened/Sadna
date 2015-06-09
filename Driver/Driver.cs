using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using ForumLoggers;


namespace Driver
{
    public class Driver
    {
        private static IApplicationBridge bridge;


        public static void Main(string[] args)
        {
            bridge = new BridgeReal();
           

            while (true)
            {
                System.Console.WriteLine("server is ");

            }
            //ForumLogger.GetInstance().Shutdown();
        }
    }
}
