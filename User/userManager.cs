using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace User
{
    class UserManager : IUserManager
    {
        String memberID;
        String memberUsername;
        private String memberPassword;
        Boolean loggerStatus = false;

         Boolean login(String inputID, String inputPassowd)
        {
            if ((inputID.CompareTo(memberID) == 1) && (inputPassowd.CompareTo(memberPassword) == 1))
            {
                loggerStatus = false;
                return true;
            }
            return false;
        }


         void logout()
        {
            if (loggerStatus == true)
                loggerStatus = false;
        }

         Boolean changePassword(String oldPass, String newPass)
        {
            if (oldPass.CompareTo(memberPassword) == 1)
            {
                if (newPass.Length < 8) // depends on the the passwords strength
                    return false;
                memberPassword = newPass;
                return true;
            }
            return false;
        }

         Boolean changeUserName(String newUserName)
        {
            Boolean prevLoggerStatus = loggerStatus;
            loggerStatus = false;
            if (prevLoggerStatus == true)
            {// if(searchForUser(newUserName)==false){
                memberUsername = newUserName;
                loggerStatus = prevLoggerStatus;
                return true;
            }

            else return false;
        }


    }
}
