using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interfaces
{
    public interface IForumManager
    {
        int CreateForum(string name);
        void RemoveForum(int forumId);
        Boolean RemoveSubForum(int forumId, int subForumId, int callerUserId);
        //return -1 for already exist topic and -2 for not admin caller.
        int CreateSubForum(string topic, int forumId);
        void AddAdmin(int userId, int forumId);
        void RemoveAdmin(int userId, int forumId);
        Boolean IsAdmin(int userId, int forumId);
        // return -1 for invalid password and -2 for already exist username
        int Register(string username, string password, string email, int forumId);
        void UnRegister(int userId, int forumId);
        int Login(string username, string password, int forumId);
        Boolean Logout(int userId, int forumId);
        void SetPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength, int forumId);
        Boolean IsModerator(int userId, int forumId, int subForumId);
        void AddModerator(int userId, int forumID, int subForumId, int callerId);
        void RemoveModerator(int userId, int forumID, int subForumId);
        void SetTopic(string topic, int forumID, int subForumId);
        int GetForumId(string name);
        int GetSubForumId(int forumId, string topic);
        int NumOfForums();
        List<int> GetForumIds();
        List<string> GetForumTopics();
        string GetForumName(int forumId);
        List<int> GetSubForumsIds(int forumId);
        List<string> GetSubForumsTopics(int forumId);
        Boolean isRegisteredUser(int forumId, int userId);
        string GetSubForumTopic(int forumId, int subForumId);
        List<int> GetAllComments(int forumId, int subForumId,int messageId);
        string GetUserType(int forumId, int userId);
        string GetUsername(int forumId, int userId);
    } 
}

