using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Driver
{
    public class BridgeReal : IApplicationBridge
    {
        public int CreateForum(/*int forumAdmin,*/ string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            throw new NotImplementedException();
        }

        public void SetPolicy(int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength, int forumId)
        {
            throw new NotImplementedException();
        }

        public int Register(string username, string password, string email, int forumId)
        {
            throw new NotImplementedException();
        }

        public int Login(string username, string password, int forumId)
        {
            throw new NotImplementedException();
        }

        public bool Logout(int userId, int forumId)
        {
            throw new NotImplementedException();
        }

        public int CreateSubForum(string topic, int forumId, int callerUserId)
        {
            throw new NotImplementedException();
        }

        public void View(int forumId, int subForumId, out string[] subForumNames, out int[] subForumIds)
        {
            throw new NotImplementedException();
        }

        public int Publish(int forumId, int subForumId, int publisherID, string title, string body)
        {
            throw new NotImplementedException();
        }

        public int Comment(int firstMessageId, int publisherID, string title, string body)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void RemoveForum(int forumId)
        {
            throw new NotImplementedException();
        }

        public void AddModerator(int forumId, int subForumId, int moderatorId)
        {
            throw new NotImplementedException();
        }

        public void RemoveModerator(int forumId, int subForumId, int moderatorId)
        {
            throw new NotImplementedException();
        }
    }
}
