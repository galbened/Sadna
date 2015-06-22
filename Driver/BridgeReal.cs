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
using ForumLoggers;

namespace Driver
{
    public class BridgeReal : IApplicationBridge
    {
        private static IUserManager UM;
        private static IForumManager FM;
        private static IMessageManager MM;
        private ForumLogger _logger;

        public BridgeReal()
        {
            _logger = ForumLogger.GetInstance();
            _logger.Write(ForumLogger.TYPE_INFO, "Initializing Driver");          
            UM = UserManager.Instance;
            _logger.Write(ForumLogger.TYPE_INFO, "User Manager initialized successfully"); 
            FM = ForumManager.getInstance();
            _logger.Write(ForumLogger.TYPE_INFO, "Forum Manager initialized successfully"); 
            MM = MessageManager.Instance();
            _logger.Write(ForumLogger.TYPE_INFO, "Message Manager initialized successfully"); 
        }


        public int CreateForum(/*int forumAdmin,*/ string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to create forum " + name);
            int forumId = -1;
            try
            {
                forumId = FM.CreateForum(name);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "CreateForum for forum " + name + " creation failed: " + ex.Message);
                throw ex;
            }
            SetPolicy(forumId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            _logger.Write(ForumLogger.TYPE_INFO, "Forum " + name + " created successfully"); 
            return forumId;
        }

        public void SetPolicy(int forumId, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Setting policy to forumId: " + forumId);
            try
            {
                FM.SetPolicy(numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength, forumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "SetPolicy to forumId " + forumId + " failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "Policy for forumId: " + forumId + " updated successfully"); 
        }

        public int Register(string username, string password, string email, int forumId)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to register " + forumId + " updated successfully");
            int userId;
            try
            {
                userId = FM.Register(username, password, email, forumId);
            }
            catch(Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Register to username " + username + " failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "Registration of " + username + " to forum " + forumId +" completed successfully"); 
            return userId;
        }

        public int Login(string username, string password, int forumId)
        {
            _logger.Write(ForumLogger.TYPE_INFO, username + " is trying to login to forum " + forumId);
            int userId;
            try
            {
                userId = FM.Login(username, password, forumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Login for username " + username + "to forum " + forumId + " failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, username + " logged to forum " + forumId + " successfully"); 
            return userId;
        }

        public bool Logout(int userId, int forumId)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "UserId " +userId + " is trying to logout from forum " + forumId);
            bool success;
            try
            {
                success = FM.Logout(userId, forumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Logout for userId " + userId + "from forum " + forumId + " failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "UserId " + userId + " disconnected from forumId " + forumId + " successfully"); 
            return success;
        }

        public int CreateSubForum(int forumId, string topic)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to create subForum " + topic + " in forum " + forumId);
            int subForumId;
            try
            {
                subForumId = FM.CreateSubForum(topic, forumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "SubForum " + topic + " creation failed in forum " + forumId + " : " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "SubForum " + topic + " created successfully in forumId " + forumId);          
            return subForumId;
        }

        public void View(int forumId, out List<string> subForumNames, out List<int> subForumIds)
        {         
            throw new NotImplementedException();
        }

        public int Publish(int forumId, int subForumId, int publisherID, string publisherName, string title, string body)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to publish thread with title " + title + " in forum " + forumId + ", subForum: " + subForumId);
            int messageId;
            try
            {
                messageId = MM.addThread(forumId, subForumId, publisherID, publisherName, title, body);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Thread publishment with title: " + title + " in forum " + forumId + ", subForum: " + subForumId +" failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "Thread was published successfully in forum " + forumId + ", subForum: " + subForumId);
            return messageId;
        }

        public int Comment(int firstMessageId, int publisherID, string publisherName, string title, string body)
        {
            _logger.Write(ForumLogger.TYPE_INFO, publisherName +" trying to publish response message for thread " + firstMessageId);
            int messageId;
            try
            {
                messageId = MM.addComment(firstMessageId, publisherID, publisherName, title, body);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Response message publishment failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, publisherName + " published new response message for thread " + firstMessageId);
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
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to delete messageId " + messageId);
            bool success;
            try
            {
                success = MM.deleteMessage(messageId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Message " +messageId+" deletion failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "Message " + messageId + " deleted successfully");         
            return success;
        }

        public void RemoveForum(int forumId)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to delete forumId " + forumId);
            try
            {
                FM.RemoveForum(forumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Forum " +forumId +" deletion failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "forumId " + forumId + " deleted successfully"); 
        }

        public void AddModerator(int forumId, int subForumId, int moderatorId)
        {
           // FM.AddModerator(moderatorId, forumId, subForumId);
        }

        public void RemoveModerator(int forumId, int subForumId, int moderatorId)
        {
            _logger.Write(ForumLogger.TYPE_INFO, "Trying to remove moderator " + moderatorId + " from forumId " + forumId + ", subForumId: " + subForumId);
            try
            {
                FM.RemoveModerator(moderatorId, forumId, subForumId);
            }
            catch (Exception ex)
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Moderator " + moderatorId + " removing from forumId " +forumId+ ", subForum: " +subForumId+" failed: " + ex.Message);
                throw ex;
            }
            _logger.Write(ForumLogger.TYPE_INFO, "Moderator " +moderatorId +" removed successfully from forumId "  +forumId+ ", subForumId: " +subForumId);
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

        public Boolean isRegisteredUser(int userId, int forumId)
        {
            return UM.isUserRegistered(userId);
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

        public void Deactivate(int userId)
        {
            UM.deactivate(userId);
        }

        public Boolean isUserRegistered(int userId)
        {
            return UM.isUserRegistered(userId);
        }
    }
}
