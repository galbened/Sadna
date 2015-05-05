using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IApplicationBridge
    {
      
        int CreateForum(/*int forumAdmin,*/ string name, 
                       int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength);

        void SetPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength, int forumId);

        //public void Entry(int forumId);

        int Register(string username, string password, string email, int forumId);

        int Login(String username, String password, int forumId);

        bool Logout(int userId, int forumId);

        int CreateSubForum(string topic, int forumId, int callerUserId);

        void View(int forumId, int subForumId, out string[] subForumNames, out int[] subForumIds);

        int Publish(int forumId, int subForumId, int publisherID, string title, string body);

        int Comment(int firstMessageId, int publisherID, string title, string body);

        void SendFriendRequest();

        void ComplainModerator();

        bool DeleteMessage(int messageId);

        void RemoveForum(int forumId);

        void AddModerator(int forumId, int subForumId, int moderatorId);

        void RemoveModerator(int forumId, int subForumId, int moderatorId);





    }
}
