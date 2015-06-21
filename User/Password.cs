using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Password
    {
        public int id { get; set; }
        public string passString { get; set; }

        public Password()
        {}
        public Password(string str)
        {
            passString = str;
        }   
    }
}
