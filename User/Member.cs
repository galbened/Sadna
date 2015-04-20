using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class Member
    {
        int memberID;
        
        public int getMemberID()
        {
            return memberID;
        }

        public String getMemberUsername()
        {
            return memberUsername;
        }

        String memberUsername;
        //private String memberPassword;
        String memberPassword;
        String memberEmail;
        Boolean loggerStatus = false;
        Boolean accountStatus;
    }
}
