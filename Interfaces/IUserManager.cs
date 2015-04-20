using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserManager
    {
        int login(String username, String password); // returns the user's ID number,-1 if the user already logged in

        Boolean logout(int userID); // returns false if the user is already offline, true otherwise

        int register(String username, String password, String email);// returns the user's ID number, -1 if the username is already taken

        Boolean enterForum(String forumName);// not sure that the right class for that

        int changePassword(String username, String oldPassword, String newPassword);//returns the user's id, -1 if false

        int changeUsername(String oldUsername, String newUsername, String password);// returns the user's id, -1 if false

        Boolean addFriend(int userID ,int friendID);// returns true if the friend request was sent, false otherwise

        String getUsername(int userID); // returns the user name, null if the user is not registered

        String getPassword(int userID); // returns the user's password, for tests only - not available in the user's side
 
        void removeFriend(int friendID);

        void approveRequest(int notificationID);



        //void complaint(notificationStatus complaint, int userID);// notificationStatus yet to be written

        void deactivate();// deletes the user account, user must be logged in
      
    }
}
