using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class stateNormal : memberState
    {
        public bool isAllowedToBeAdmin()
        {
            return false;
        }

        public string getType()
        {
            return "Normal";
        }
    }
}
