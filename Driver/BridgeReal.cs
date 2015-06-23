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
        private static BridgeReal instance=null;
        private ForumLogger _logger;
  

        private BridgeReal()
        {
            _logger = ForumLogger.GetInstance();
            _logger.Write(ForumLogger.TYPE_INFO, "Initializing Driver"); 
            UM = UserManager.Instance;
            _logger.Write(ForumLogger.TYPE_INFO, "User Manager initialized successfully"); 
            FM = ForumManager.getInstance();
            _logger.Write(ForumLogger.TYPE_INFO, "Forum Manager initialized successfully"); 
            MM = MessageManager.Instance();
            _logger.Write(ForumLogger.TYPE_INFO, "Message Manager initialized successfully");
            _logger.Write(ForumLogger.TYPE_INFO, "Initializing Data"); 
            initializingDemoRunData();
            _logger.Write(ForumLogger.TYPE_INFO, "Data initialized successfully"); 
        }

        public static BridgeReal GetInstance()
        {
            if (instance == null)
            {
                instance = new BridgeReal();
            }
            return instance;
        }


        public int CreateForum(int userRequesterId, string name, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            int forumId = FM.CreateForum(userRequesterId, name);
            SetPolicy(userRequesterId, forumId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength);
            return forumId;
        }

        public void SetPolicy(int userRequesterId, int forumId, int numOfModerators, string degreeOfEnsuring, bool uppercase, bool lowercase, bool numbers, bool symbols, int minLength)
        {
            FM.SetPolicy(userRequesterId, numOfModerators, degreeOfEnsuring, uppercase, lowercase, numbers, symbols, minLength, forumId);
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

        public int CreateSubForum(int userRequesterId, int forumId, string topic)
        {
            int subForumId = FM.CreateSubForum(userRequesterId, topic, forumId);
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

        public bool DeleteMessage(int userRequesterId, int messageId)
        {
            bool success = MM.deleteMessage(userRequesterId, messageId);
            return success;
        }

        public void RemoveForum(int userRequesterId, int forumId)
        {
            FM.RemoveForum(userRequesterId, forumId);
        }

        public void AddModerator(int userRequesterId, int forumId, int subForumId, int moderatorId)
        {
           // FM.AddModerator(moderatorId, forumId, subForumId);
        }

        public void RemoveModerator(int userRequesterId, int forumId, int subForumId, int moderatorId)
        {
            FM.RemoveModerator(userRequesterId, moderatorId, forumId, subForumId);
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

        public void initializingDemoRunData()
        {
            int forum_sports = FM.CreateForum(1, "Sports");
            int forum_news = FM.CreateForum(1, "News");

            int subforum_1_sports = FM.CreateSubForum(1, "Soccer", forum_sports);
            int subforum_2_sports = FM.CreateSubForum(1, "Basketball", forum_sports);
            int subforum_3_sports = FM.CreateSubForum(1, "Tennis", forum_sports);

            int subforum_1_news = FM.CreateSubForum(1, "Domestic", forum_news);
            int subforum_2_news = FM.CreateSubForum(1, "Abroad", forum_news);
            int subforum_3_news = FM.CreateSubForum(1, "Politics", forum_news);
            int subforum_4_news = FM.CreateSubForum(1, "Weather", forum_news);

            int user_1_sports = FM.Register("user_1_sports", "user_1_sports_bpass", "user_1_sports@mail.com", forum_sports);
            int user_2_sports = FM.Register("user_2_sports", "user_2_sports_bpass", "user_2_sports@mail.com", forum_sports);
            int user_3_sports = FM.Register("user_3_sports", "user_3_sports_bpass", "user_3_sports@mail.com", forum_sports);
            int user_1_news = FM.Register("user_1_news", "user_1_news_bpass", "user_1_news@mail.com", forum_news);
            int user_2_news = FM.Register("user_2_news", "user_2_news_bpass", "user_2_news@mail.com", forum_news);
            int user_3_news = FM.Register("user_3_news", "user_3_news_bpass", "user_3_news@mail.com", forum_news);
            int user_4_news = FM.Register("user_4_news", "user_4_news_bpass", "user_4_news@mail.com", forum_news);

            int thread_1_subforum_1_sports = MM.addThread(forum_sports, subforum_1_sports, user_1_sports, UM.getUsername(user_1_sports), "message title 1", "message body 1");
            MM.addComment(thread_1_subforum_1_sports, user_2_sports, UM.getUsername(user_2_sports), "reponse message title 1 2", "response message body 1 2");
            MM.addComment(thread_1_subforum_1_sports, user_3_sports, UM.getUsername(user_3_sports), "reponse message title 1 3", "response message body 1 3");

            int thread_1_subforum_1_news = MM.addThread(forum_news, subforum_1_news, user_1_news, UM.getUsername(user_1_news), "message title 1", "message body 1");
            MM.addComment(thread_1_subforum_1_news, user_2_news, UM.getUsername(user_2_news), "reponse message title 1 2", "response message body 1 2");
            MM.addComment(thread_1_subforum_1_news, user_3_news, UM.getUsername(user_3_news), "reponse message title 1 3", "response message body 1 3");
            MM.addComment(thread_1_subforum_1_news, user_4_news, UM.getUsername(user_4_news), "reponse message title 1 4", "response message body 1 4");

            FM.AddAdmin(1,user_1_sports, forum_sports);

            /*
            List<int> forumIds = new List<int>();
            List<int> subForumIds = new List<int>();
            List<int> adminUsersIds = new List<int>();
            List<int> regularUsersIds = new List<int>();
            # region forum creation
            // two moderators,upper/lower/numbers 3 minimum
            forumIds.Add(CreateForum("Youtube Fail army ", 2, "", true, true, true, false, 3));

            // one moderators,lower/numbers 4 minimum
            forumIds.Add(CreateForum("Eliad Malki VEVO", 1, "", false, true, true, false, 4));

            // four moderators,upper/lower/numbers/symbols 5 minimum
            forumIds.Add(CreateForum("SE workshop", 4, "", false, true, true, true, 5));
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
            adminUsersIds.Add(Register("fail army Admin", "fa1L100", "failarmy@gmail.com", forumIds[0]));
            regularUsersIds.Add(Register("Gal Porat", "ga1PoPo", "galpo@gmail.com", forumIds[0]));


            adminUsersIds.Add(Register("Eliad Malki Admin", "B0nb0n", "mamimami@gmail.com", forumIds[1]));
            regularUsersIds.Add(Register("Osher Damari", "s3xyT1m3", "osherda@gmail.com", forumIds[1]));

            adminUsersIds.Add(Register("Gal Ben Admin", "Ar0ma1989", "bened@gmail.com", forumIds[2]));
            regularUsersIds.Add(Register("Tomer Segal", "Yukukuku33", "tomerse@gmail.com", forumIds[2]));
            # endregion

            # region adding messages
            // adding two messages to each sub forum,
            // each message has at least one comment

            //Publish(forumIds[0],subForumIds[0],


            #endregion 
            */
        }
    }
}
