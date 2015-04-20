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
        
        public UserManager()
        {
            UsersList = new List<Member>();
        }

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
