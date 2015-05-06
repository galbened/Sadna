using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    interface memberState
    {
        public bool isAllowedToBeAdmin();   //  for later methods
        public String getType();            //  for nextState() and previousState() that are implemented in the Context Class



    }
}
