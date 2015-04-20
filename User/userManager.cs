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
        List<Member> UsersList = new List<Member>();
        
        /*
         Boolean login(String inputID, String inputPassowd)
        {
             
            if ((inputID.CompareTo(memberID) == 1) && (inputPassowd.CompareTo(memberPassword) == 1))
            {
                loggerStatus = false;
                return true;
            }
            return false;
              
            return false;
        }


         void logout()
        {
             
            if (loggerStatus == true)
                loggerStatus = false;
              
              
              
            return;
        }

        int register(String username, String password, String email)
         {
            //TODO
             return 0;
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
              
            return false;
        }
        */


        int IUserManager.login(string username, string password)
        {
            throw new NotImplementedException();
        }

        bool IUserManager.logout(int userID)
        {
            throw new NotImplementedException();
        }

        int IUserManager.register(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        bool IUserManager.enterForum(string forumName)
        {
            throw new NotImplementedException();
        }

        int IUserManager.changePassword(int userID, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        int IUserManager.changeUsername(int userID, string newUsername, string password)
        {
            throw new NotImplementedException();
        }

        int IUserManager.addFriend(int userID, int friendID)
        {
            throw new NotImplementedException();
        }

        string IUserManager.getUsername(int userID)
        {
            throw new NotImplementedException();
        }

        string IUserManager.getPassword(int userID)
        {
            throw new NotImplementedException();
        }

        void IUserManager.removeFriend(int userID, int friendID)
        {
            throw new NotImplementedException();
        }

        void IUserManager.approveRequest(int notificationID)
        {
            throw new NotImplementedException();
        }

        void IUserManager.deactivate(int userID)
        {
            throw new NotImplementedException();
        }

        //Helper methods

        /*
         * returns Member instance that matches the given userID, throws exception if ID not found.
         */
        private Member getMemberByID(int userID)
        {
            foreach (Member member in UsersList)
            {
                if (member.getMemberID() == userID)
                    return member; 
            }
            throw new System.InvalidOperationException("userID not found.");
        }
    }
}
