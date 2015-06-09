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

        void SetPolicy(int forumId, int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength);

        //public void Entry(int forumId);

        int Register(string username, string password, string email, int forumId);

        int Login(String username, String password, int forumId);

        bool Logout(int userId, int forumId);

        int CreateSubForum(int forumId, string topic);

        void View(int forumId, out List<string> subForumNames, out List<int> subForumIds);

        int Publish(int forumId, int subForumId, int publisherID, string title, string body);

        int Comment(int firstMessageId, int publisherID, string title, string body);

        void SendFriendRequest();

        void ComplainModerator();

        bool DeleteMessage(int messageId);

        void RemoveForum(int forumId);

        void AddModerator(int forumId, int subForumId, int moderatorId);

        void RemoveModerator(int forumId, int subForumId, int moderatorId);

        List<int> GetForumIds();

        List<string> GetForumTopics();

        List<int> getRegisteredUsers(int forumId);

        List<int> getAllThreads(int forumId, int subForumId);

        List<int> getAllComments(int forumId,int subForumId, int messageId);// should throw exception in case of missing messageId

        List<int> getModerators(int forumId, int subForumId);

        string GetForumName(int forumId);

        List<int> GetSubForumsIds(int forumId);

        List<string> GetSubForumsTopics(int forumId);

    }
}
