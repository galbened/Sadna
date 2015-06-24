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

        int register(string username, string password, string email);// returns the user's ID number, -1 if the username is already taken

        Boolean enterForum(String forumName);// not sure that the right class for that

        int changePassword(int userID, String oldPassword, String newPassword);//returns the user's id, -1 if false

        int changeUsername(int userID, String newUsername, String password);// returns the user's id, -1 if false

        int addFriend(int userID ,int friendID);// returns friend's notification ID, -1 if error

        String getUsername(int userID); // returns the user name, null if the user is not registered

        String getPassword(int userID); // returns the user's password, for tests only - not available in the user's side
 
        void removeFriend(int userID, int friendID);

        void approveRequest(int notificationID);

        bool IsPasswordValid(string username, int expectancy);

        bool isLoggedin(int userId);


        //void complaint(notificationStatus complaint, int userID);// notificationStatus yet to be written

        void deactivate(int userID);// deletes the user account, user must be logged in

        Boolean getConfirmationCodeFromUser(int userID, int code); // input: user enters the code he received by mail.
                                                                   // compares the given code with the real one.
                                                                   // if true, activates account and returns true, false, returns false 

        Boolean isUserRegistered(int userId);

        string GetUserMail(int userId);

        int GetUserId(string userName);
    }
}
