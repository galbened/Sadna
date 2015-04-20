using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserManager
    {
        int login(String username, String password); // returns the user's ID number,0 if the user already logged in

        Boolean logout(int userID, String password); // returns false if the user is already offline, true otherwise

        int register(String username, String password);// returns the user's ID number, 0 if the username is already taken

        Boolean enterForum(String forumName);// not sure that the right class for that

        Boolean changePassword(String username, String oldPassword, String newPassword);//returns false if the password is not legal

        Boolean changeUsername(String oldUsername, String newUsername, String password);// returns false if the user name is taken

        Boolean addFriend(int userID ,int friendID);// returns true if the friend request was sent, false otherwise
 
        void removeFriend(int friendID);

        void approveRequest(int notificationID);

        //void complaint(notificationStatus complaint, int userID);// notificationStatus yet to be written

        void deactivate();// deletes the user account, user must be logged in
      
    }
}
