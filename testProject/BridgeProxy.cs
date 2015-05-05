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

        public void SetPolicy(int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength, int forumId)
        {
            if (_real != null)
                _real.SetPolicy(numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength, forumId);
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

        public int CreateSubForum(string topic, int forumId, int callerUserId)
        {
            if (_real != null)
                return _real.CreateSubForum(topic, forumId, callerUserId);
            throw new NullReferenceException("_real in null");
        }

        public void View(int forumId, int subForumId, out string[] subForumNames, out int[] subForumIds)
        {
            if (_real != null)
                _real.View(forumId, subForumId, out subForumNames, out subForumIds);
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
    }
}
