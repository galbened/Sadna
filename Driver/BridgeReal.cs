using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Message;
using User;
using Forum;
using Notification;

namespace Driver
{
    public class BridgeReal : IApplicationBridge
    {
        private static List<int> usersIds;
        private static List<int> forumsIds;
        private static List<int> subForumsIds;
        private static List<int> messagesIds;
        private static IUserManager UM;
        private static IForumManager FM;
        private static IMessageManager MM;
  

        public BridgeReal()
        {
            usersIds = new List<int>();
            forumsIds = new List<int>();
            subForumsIds = new List<int>();
            messagesIds = new List<int>();
            UM = UserManager.Instance();
            FM = ForumManager.getInstance();
            MM = MessageManager.Instance();
        }


        public int CreateForum(/*int forumAdmin,*/ string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            int forumId = FM.CreateForum(name);
            SetPolicy(forumId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            return forumId;
        }

        public void SetPolicy(int forumId, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            FM.SetPolicy(numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength, forumId);
        }

        public int Register(string username, string password, string email, int forumId)
        {
            int userId = FM.Register(username, password, email, forumId);
            return userId;
        }

        public int Login(string username, string password, int forumId)
        {
            int userId = FM.Login(username, password, forumId);
            return userId;
        }

        public bool Logout(int userId, int forumId)
        {
            bool success = FM.Logout(userId, forumId);
            return success;
        }

        public int CreateSubForum(int forumId, string topic)
        {
            int subForumId = FM.CreateSubForum(topic, forumId);
            return subForumId;
        }

        public void View(int forumId, out List<string> subForumNames, out List<int> subForumIds)
        {         
            throw new NotImplementedException();
        }

        public int Publish(int forumId, int subForumId, int publisherID, string title, string body)
        {
            int messageId = MM.addThread(forumId, subForumId, publisherID, title, body);
            return messageId;
        }

        public int Comment(int firstMessageId, int publisherID, string title, string body)
        {
            int messageId = MM.addComment(firstMessageId, publisherID, title, body);
            return messageId;
        }

        public void SendFriendRequest()
        {
            throw new NotImplementedException();
        }

        public void ComplainModerator()
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessage(int messageId)
        {
            bool success = MM.deleteMessage(messageId);
            return success;
        }

        public void RemoveForum(int forumId)
        {
            FM.RemoveForum(forumId);
        }

        public void AddModerator(int forumId, int subForumId, int moderatorId)
        {
           // FM.AddModerator(moderatorId, forumId, subForumId);
        }

        public void RemoveModerator(int forumId, int subForumId, int moderatorId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetForumIds()
        {
            throw new NotImplementedException();
        }

        public List<string> GetForumTopics()
        {
            throw new NotImplementedException();
        }
    }
}
