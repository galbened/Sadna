using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    interface userManagment
    {
        Boolean login(String username, String password);
        int register(String username, String password);
        Boolean enterForum(String forumName);// not sure that the right class for that
        Boolean changePassword(String username, String oldPassword, String newPassword);
        Boolean changeUsername(String oldUsername, String newUsername, String password);
        Boolean addFriend(int userID ,int friendID);
        void removeFriend(int friendID);
        void approveRequest(int notificationID);
        void complaint(notificationStatus complaint, int userID);
        void deactivate();
        //for upload
    }
}
