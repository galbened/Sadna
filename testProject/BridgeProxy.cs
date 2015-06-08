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

        public int CreateForum(/*int forumAdmin,*/ string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            if (_real != null)
                return _real.CreateForum(/*forumAdmin,*/ name, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            throw new NullReferenceException("_real in null");
        }

        public void SetPolicy(int forumId, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            if (_real != null)
                _real.SetPolicy(forumId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
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

        public int CreateSubForum(int forumId, string topic)
        {
            if (_real != null)
                return _real.CreateSubForum(forumId, topic);
            throw new NullReferenceException("_real in null");
        }

        public void View(int forumId, out List<string> subForumNames, out List<int> subForumIds)
        {
            if (_real != null)
                _real.View(forumId, out subForumNames, out subForumIds);
            throw new NullReferenceException("_real in null");
        }

        public int Publish(int forumId, int subForumId, int publisherID, string title, string body)
        {
            if (_real != null)
                _real.Publish(forumId, subForumId, publisherID, title, body);
            throw new NullReferenceException("_real in null");
        }

        public int Comment(int firstMessageId, int publisherID, string title, string body)
        {
            if (_real != null)
                _real.Comment(firstMessageId, publisherID, title, body);
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

        public bool DeleteMessage(int messageId)
        {
            if (_real != null)
                _real.DeleteMessage(messageId);
            throw new NullReferenceException("_real in null");
        }

        public void RemoveForum(int forumId)
        {
            if (_real != null)
                _real.RemoveForum(forumId);
            throw new NullReferenceException("_real in null");
        }

        public void AddModerator(int forumId, int subForumId, int moderatorId)
        {
            if (_real != null)
                _real.AddModerator(forumId, subForumId, moderatorId);
            throw new NullReferenceException("_real in null");
        }

        public void RemoveModerator(int forumId, int subForumId, int moderatorId)
        {
            if (_real != null)
                _real.RemoveModerator(forumId, subForumId, moderatorId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> GetForumIds()
        {
            if (_real != null)
                _real.GetForumIds();
            throw new NullReferenceException("_real in null");
        }

        public List<string> GetForumTopics()
        {
            if (_real != null)
                _real.GetForumTopics();
            throw new NullReferenceException("_real in null");
        }

        public List<int> getRegisteredUsers(int forumId)
        {
            if (_real != null)
                _real.getRegisteredUsers(forumId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> getSubForums(int forumId)
        {
            if (_real != null)
                _real.getSubForums(forumId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> getAllThreads(int forumId)
        {
            if (_real != null)
                _real.getAllThreads(forumId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> getAllComments(int forumId, int messageId)
        {
            if (_real != null)
                _real.getAllComments(forumId, messageId);
            throw new NullReferenceException("_real in null");
        }

        public List<int> getModerators(int forumId)
        {
            if (_real != null)
                _real.getModerators(forumId);
            throw new NullReferenceException("_real in null");
        }
    }
}
