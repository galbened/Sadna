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

        
        /*
         * gets a pasword and compares it with the uesr's password,
         * returns true if it match
         * */
        public Boolean login(String password)
        {
            if ((password.CompareTo(memberPassword) == 1) && (loggerStatus == false))
            {
                loggerStatus = true;
                return true;
            }
            return false;
        }

        String memberUsername;
        //private String memberPassword;
        String memberPassword;
        String memberEmail;
        Boolean loggerStatus = false;

        public Boolean getLoggerStatus()
        {
            return loggerStatus;
        }

        public void setLoggerStatus(Boolean wantedStatus) //sets status false if logging out, true if logging in
        {
            loggerStatus = wantedStatus;
        }

        Boolean accountStatus;
    }
}
