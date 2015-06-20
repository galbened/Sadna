using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public interface memberState
    {
         bool isAllowedToBeAdmin();   //  for later methods
         String getType();            //  for nextState() and previousState() that are implemented in the Context Class

        // 2 Do : add some methods that are requires some member type

    }
}
