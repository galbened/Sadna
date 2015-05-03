using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IApplication
    {
      
        public int CreateForum(int forumAdmin, string name, 
                       int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength);

        public void SetPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength, int forumId);

        //public void Entry(int forumId);

        public int Register(string username, string password, string email);

        public int Login(String username, String password, int forumId);

        public bool Logout(int userId, int forumId);

        public int CreateSubForum(string topic, int forumId, int callerUserId);

        public void View(int forumId, int subForumId, out string[] subForumNames, out int[] subForumIds);

        public int Publish(int forumId, int subForumId, int publisherID, string title, string body);

        public int Comment(int firstMessageId, int publisherID, string title, string body);

        public void SendFriendRequest();

        public void ComplainModerator();

        public bool DeleteMessage(int messageId);

        public void RemoveForum(int forumId);

        public void AddModerator(int forumId, int subForumId, int moderatorId);

        public void RemoveModerator(int forumId, int subForumId, int moderatorId);





    }
}
