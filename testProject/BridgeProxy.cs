using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace testProject
{
    class BridgeProxy : IApplicationBridge
    {
        private IApplicationBridge _real;

        public BridgeProxy()
        {
            _real = null;
        }

        public void SetRealBridge(IApplicationBridge real)
        {
            _real = real;
        }

        public int CreateForum(/*int forumAdmin,*/int userId, string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            if (_real != null)
                return _real.CreateForum(/*forumAdmin,*/ 1, name, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            throw new NullReferenceException("_real in null");
        }

        public void SetPolicy(int userId,int forumId, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            if (_real != null)
                _real.SetPolicy(1, forumId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            throw new NullReferenceException("_real in null");
        }

        public int Register(string username, string password, string email, int forumId)
        {
            if (_real != null)
                return _real.Register(username, password, email, forumId);
            throw new NullReferenceException("_real in null");
        }

        public int Login(string username, string password, int forumId)
        {
            if (_real != null)
                return _real.Login(username, password, forumId);
            throw new NullReferenceException("_real in null");
        }

        public bool Logout(int userId, int forumId)
        {
            if (_real != null)
                return _real.Logout(userId, forumId);
            throw new NullReferenceException("_real in null");
        }

        public int CreateSubForum(int userId,int forumId, string topic)
        {
            if (_real != null)
                return _real.CreateSubForum(1, forumId, topic);
            throw new NullReferenceException("_real in null");
        }

        public int ChangePassword(int userID, string oldPassword, string newPassword)
        {
            if (_real != null)
                return _real.ChangePassword( userID,  oldPassword,  newPassword);
            throw new NullReferenceException("_real in null");
        }

        public int ChangeUsername(int userID, string newUsername, string password)
        {
            if (_real != null)
                return _real.ChangeUsername(userID, newUsername, password);
            throw new NullReferenceException("_real in null");
        }
        public void View(int forumId, out List<string> subForumNames, out List<int> subForumIds)
        {
            if (_real != null)
                _real.View(forumId, out subForumNames, out subForumIds);
            throw new NullReferenceException("_real in null");
        }

        public int Publish(int forumId, int subForumId, int publisherID, string publisherName, string title, string body)
        {
            if (_real != null)
                return _real.Publish(forumId, subForumId, publisherID, publisherName, title, body);
            throw new NullReferenceException("_real in null");
        }

        public int Comment(int firstMessageId, int publisherID, string publisherName, string title, string body)
        {
            if (_real != null)
                return _real.Comment(firstMessageId, publisherID, publisherName, title, body);
            throw new NullReferenceException("_real in null");
        }

        public void SendFriendRequest()
        {
            if (_real != null)
                _real.SendFriendRequest();
            throw new NullReferenceException("_real in null");
        }

        public void ComplainModerator()
        {
            if (_real != null)
                _real.ComplainModerator();
            throw new NullReferenceException("_real in null");
        }

        public bool DeleteMessage(int userId,int messageId)
        {
            if (_real != null)
                return _real.DeleteMessage(1, messageId);
            throw new NullReferenceException("_real in null");
        }

        public void RemoveForum(int userId,int forumId)
        {
            if (_real != null)
                _real.RemoveForum(1, forumId);
            throw new NullReferenceException("_real in null");
        }

        public void AddModerator(int userId,int forumId, int subForumId, int moderatorId)
        {
            if (_real != null)
                _real.AddModerator(1, forumId, subForumId, moderatorId);
            throw new NullReferenceException("_real in null");
        }

        public void RemoveModerator(int userId,int forumId, int subForumId, int moderatorId)
        {
            if (_real != null)
                _real.RemoveModerator(1, forumId, subForumId, moderatorId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> GetForumIds()
        {
            if (_real != null)
                return _real.GetForumIds();
            throw new NullReferenceException("_real in null");
        }

        public List<string> GetForumTopics()
        {
            if (_real != null)
                return _real.GetForumTopics();
            throw new NullReferenceException("_real in null");
        }


        public string GetUsername(int userId)
        {
            if (_real != null)
                return _real.GetUsername( userId);
            throw new NullReferenceException("_real in null");
        }

        public Boolean isRegisteredUser(int forumId,int userId)
        {
            if (_real != null)
                return _real.isRegisteredUser(forumId, userId);
            throw new NullReferenceException("_real in null");
        }

        public List<ThreadInfo> GetAllThreads(int forumId, int subForumId)
        {
            if (_real != null)
                return _real.GetAllThreads(forumId, subForumId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> GetAllComments(int forumId, int subForumId, int firstMessageId)
        {
            if (_real != null)
                return _real.GetAllComments(forumId, subForumId, firstMessageId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> GetModerators(int forumId, int subForumId)
        {
            if (_real != null)
                return _real.GetModerators(forumId, subForumId);
            throw new NullReferenceException("_real in null");
        }

        public string GetForumName(int forumId)
        {
            if (_real != null)
                return _real.GetForumName(forumId);
            throw new NullReferenceException("_real in null");
        }

        public List<string> GetSubForumsTopics(int forumId)
        {
            if (_real != null)
                return _real.GetSubForumsTopics(forumId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> GetSubForumsIds(int forumId)
        {
            if (_real != null)
                return _real.GetSubForumsIds(forumId);
            throw new NullReferenceException("_real in null");
        }

        public string GetSubForumTopic(int forumId, int subForumId)
        {
            if (_real != null)
                return _real.GetSubForumTopic(forumId, subForumId);
            throw new NullReferenceException("_real in null");
        }

        public bool isLoggedin(int userId)
        {
            if (_real != null)
                return _real.isLoggedin(userId);
            throw new NullReferenceException("_real in null");
        }

        public string GetUserType(int forumId, int userId)
        {
            if (_real != null)
                return _real.GetUserType(forumId, userId);
            throw new NullReferenceException("_real in null");
        }

        public string GetUsername(int forumId, int userId)
        {
            if (_real != null)
                return _real.GetUsername(forumId, userId);
            throw new NullReferenceException("_real in null");
        }

        public List<string> GetSessionHistory(int requesterId, int forumId, int userIdSession)
        {
            if (_real != null)
                return _real.GetSessionHistory(requesterId, forumId, userIdSession);
            throw new NullReferenceException("_real in null");
        }

        public void Deactivate(int userId)
        {
            if (_real != null)
                 _real.Deactivate(userId);
            else
                throw new NullReferenceException("_real in null");
        }

        public Boolean isUserRegistered(int userId)
        {
            if (_real != null)
                return _real.isUserRegistered(userId);
            throw new NullReferenceException("_real in null");
        }
    }
}
