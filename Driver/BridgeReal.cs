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
        private static IUserManager UM;
        private static IForumManager FM;
        private static IMessageManager MM;
  

        public BridgeReal()
        {
            UM = UserManager.Instance;
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

        public int Publish(int forumId, int subForumId, int publisherID, string publisherName, string title, string body)
        {
            int messageId = MM.addThread(forumId, subForumId, publisherID, publisherName, title, body);
            return messageId;
        }

        public int Comment(int firstMessageId, int publisherID, string publisherName, string title, string body)
        {
            int messageId = MM.addComment(firstMessageId, publisherID,  publisherName, title, body);
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
            FM.RemoveModerator(moderatorId, forumId, subForumId);
        }

        public List<int> GetForumIds()
        {
            List<int> ans = FM.GetForumIds();
            return ans;
        }

        public List<string> GetForumTopics()
        {
            List<string> ans = FM.GetForumTopics();
            return ans;
        }

        public Boolean isRegisteredUser(int forumId, int userId)
        {
            return FM.isRegisteredUser(forumId, userId);
        }

        public List<int> getSubForums(int forumId)
        {
            throw new NotImplementedException();
        }

        public List<ThreadInfo> GetAllThreads(int forumId, int subForumId)
        {
            return MM.GetAllThreads(forumId, subForumId);
        }

        public List<int> GetAllComments(int forumId, int subForumId, int messageId)
        {
            return FM.GetAllComments(forumId, subForumId,messageId);
        }

        public List<int> GetModerators(int forumId, int subForumId)
        {
            throw new NotImplementedException();
        }

        public string GetForumName(int forumId)
        {
            return FM.GetForumName(forumId);
        }

        public List<int> GetSubForumsIds(int forumId)
        {
            return FM.GetSubForumsIds(forumId);
        }

        public List<string> GetSubForumsTopics(int forumId)
        {
            return FM.GetSubForumsTopics(forumId);
        }

        public string GetSubForumTopic(int forumId, int subForumId)
        {
            return FM.GetSubForumTopic(forumId, subForumId);
        }

        public string GetUsername(int userID)
        {
            return UM.getUsername(userID);
        }

        public int ChangePassword(int userID, string oldPassword, string newPassword)
        {
            return UM.changePassword( userID,  oldPassword,  newPassword);
        }

        public int ChangeUsername(int userID, string newUsername, string password)
        {
            return UM.changeUsername(userID, newUsername, password);
        }

        public bool isLoggedin(int userId)
        {
            return UM.isLoggedin(userId);
        }

        public string GetUserType(int forumId, int userId)
        {
            return FM.GetUserType(forumId, userId);
        }

        public string GetUsername(int forumId, int userId)
        {
            return FM.GetUsername(forumId, userId);
        }

        public List<string> GetSessionHistory(int requesterId, int forumId, int userIdSession)
        {
            return FM.GetSessionHistory(requesterId, forumId, userIdSession);
        }
    }
}
