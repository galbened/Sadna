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
        private static IApplicationBridge bridge;
        private static List<int> usersIds;
        private static List<int> forumsIds;
        private static List<int> subForumsIds;
        private static List<int> messagesIds;

        public static void Main(string[] args)
        {
            bridge = new BridgeReal();
            usersIds = new List<int>();
            forumsIds = new List<int>();
            subForumsIds = new List<int>();
            messagesIds = new List<int>();
            while (true)
            {
                System.Console.WriteLine("server is ");

            }
        }
    }
}
