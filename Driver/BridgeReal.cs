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

            List<int> forumIds = new List<int>();
            List<int> subForumIds = new List<int>();
            List<int> adminUsersIds = new List<int>();
            List<int> regularUsersIds = new List<int>();
            # region forum creation
            // two moderators,upper/lower/numbers 3 minimum
            forumIds.Add(CreateForum("Youtube Fail army ",2,"",true,true,true,false,3));

            // one moderators,lower/numbers 4 minimum
            forumIds.Add(CreateForum("Eliad Malki VEVO", 1, "", false, true, true, false, 4));

            // four moderators,upper/lower/numbers/symbols 5 minimum
            forumIds.Add( CreateForum("SE workshop", 4, "", false, true, true, true, 5));
            # endregion

            # region sub-forum creation
            // subforums for first forum
            subForumIds.Add(CreateSubForum(forumIds[0], "Compilation"));
            subForumIds.Add(CreateSubForum(forumIds[0], "Fails of the month"));


            // subforums for second forum
            subForumIds.Add(CreateSubForum(forumIds[1], "The songs"));
            subForumIds.Add(CreateSubForum(forumIds[1], "Video clips"));

            // subforums for third forum
            subForumIds.Add(CreateSubForum(forumIds[1], "Meetings"));
            # endregion 

            #region user_creation_and_registration
            adminUsersIds.Add(Register("fail army Admin","fa1L100","failarmy@gmail.com",forumIds[0]));
            regularUsersIds.Add(Register("Gal Porat","ga1PoPo","galpo@gmail.com",forumIds[0]));


            adminUsersIds.Add(Register("Eliad Malki Admin","B0nb0n","mamimami@gmail.com",forumIds[1]));
            regularUsersIds.Add(Register("Osher Damari","s3xyT1m3","osherda@gmail.com",forumIds[1]));

            adminUsersIds.Add(Register("Gal Ben Admin","Ar0ma1989","bened@gmail.com",forumIds[2]));
            regularUsersIds.Add(Register("Tomer Segal","Yukukuku33","tomerse@gmail.com",forumIds[2]));
            # endregion 

            # region adding messages
            // adding two messages to each sub forum,
            // each message has at least one comment
            
            //Publish(forumIds[0],subForumIds[0],


            #endregion 
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
