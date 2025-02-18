﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IApplicationBridge
    {
        
        int CreateForum(int userRequesterId, string name, 
                       int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength);

        void SetPolicy(int userRequesterId, int forumId, int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength);

        //public void Entry(int forumId);

        int Register(string username, string password, string email, int forumId);

        int Login(String username, String password, int forumId);

        bool Logout(int userId, int forumId);

        int CreateSubForum(int userRequesterId, int forumId, string topic);

        void View(int forumId, out List<string> subForumNames, out List<int> subForumIds);

        int Publish(int forumId, int subForumId, int publisherID, string publisherName, string title, string body);

        int Comment(int firstMessageId, int publisherID, string publisherName, string title, string body);

        void SendFriendRequest(int requesterId, int friendId, int forumId);

        void ComplainModerator(int userRequesterId, int moderator, int forumId, int subForumId);

        bool EditMessage(int userRequesterId, int messageId, string title, string body);

        bool DeleteMessage(int userRequesterId, int messageId);

        void RemoveForum(int userRequesterId, int forumId);

        void RemoveSubForum(int userRequesterId, int forumId, int subForumId);

        void AddAdmin(int userRequesterId, int forumId, int adminId);

        void AddModerator(int userRequesterId, int forumId, int subForumId, int moderatorId);

        void RemoveModerator(int userRequesterId, int forumId, int subForumId, int moderatorId);

        List<int> GetForumIds();

        List<string> GetForumTopics();

        Boolean isRegisteredUser(int forumId, int userId);

        List<ThreadInfo> GetAllThreads(int forumId, int subForumId);

        List<int> GetAllComments(int forumId,int subForumId, int messageId);// should throw exception in case of missing messageId

        string GetForumName(int forumId);

        List<int> GetSubForumsIds(int forumId);

        List<string> GetSubForumsTopics(int forumId);

        string GetUsername(int userId);

        string GetSubForumTopic(int forumId, int subForumId);

        int ChangePassword(int userID, string oldPassword, string newPassword);

        int ChangeUsername(int userID, string newUsername, string password);

        bool isLoggedin(int userId);

        //return "admin" if admin, "member" if member, otherwise ""
        string GetUserType(int forumId, int userId);

        //return username, otherwise throw ArgumentException 
        string GetUsername(int forumId, int userId);

        List<string> GetSessionHistory(int requesterId, int forumId, int userIdSession);

        void Deactivate(int userId);

        Boolean isUserRegistered(int userId);

        List<int> GetModeratorIds(int forumId, int subForumId);

        List<string> GetModeratorNames(int forumId, int subForumId);

        List<int> GetMembersNoAdminIds(int forumId);

        List<string> GetMembersNoAdminNames(int forumId);

        List<int> GetMembersNoModeratorIds(int forumId, int subForumId);

        List<string> GetMembersNoModeratorNames(int forumId, int subForumId);

        Boolean IsPasswordCorrect(int userId, String password);
    }
}
