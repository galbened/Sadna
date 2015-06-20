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
        public string pass { get; set; }

        public Password(string str)
        {
            pass = str;
        }   
    }
}
