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
        List<Member> UsersList;
        int newestMemberID = 1;

        int IUserManager.login(string username, string password)
        {
            foreach (Member member in UsersList)
            {
                if ((member.getMemberUsername().CompareTo(username) == 1))
                {
                    if (member.login(password) == true)
                        return member.getMemberID();

                }
                    
            }
            return -1;
        }

        bool IUserManager.logout(int userID)
        {
            Member mem = getMemberByID(userID);
            if(mem.getLoggerStatus()==false)    //is user already logged out?
                return false; 
            mem.setLoggerStatus(false);         //logging the user out.
            return true;
        }

        int IUserManager.register(string username, string password, string email)
        {
            UsersList.Add(new Member(newestMemberID, username, password, email));
            newestMemberID++;
            return newestMemberID - 1;
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
