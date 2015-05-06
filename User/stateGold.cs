using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class stateGold : memberState
    {

        public bool isAllowedToBeAdmin()
        {
            return true;
        }

        public string getType()
        {
            return ("Gold");
        }

    }
}
