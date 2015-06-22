using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using ForumLoggers;
using Message;
using System.Configuration;


namespace Forum
{
    public class ForumManager : IForumManager
    {
        private List<Forum> forums;
        private static ForumManager instance = null;
        private static int forumIdCounter;
        private IMessageManager MM;
        private const string error_emptyTitle = "Cannot create forum without title";
        private const string error_emptyTitleSub = "Cannot create subForum without topic";
        private const string error_existTitle = "Cannot create forum with already exit title";
        private const string error_forumID = "No such forum: ";
        private const string error_accessDenied = "You have no permissions to perform this operation";
        //private ForumLogger loggerIns;

        IDBManager<Forum> DBforumMan;

        private ForumManager()
        {
            forums = new List<Forum>();
            forumIdCounter = 1000;
            MM = MessageManager.Instance();
            //loggerIns = ForumLogger.GetInstance();

            
            DBforumMan = new DBforumManager();
            /* 
            DBforumMan.add(new Forum("Sports3", 1));
            DBforumMan.add(new Forum("News3", 2));
            DBforumMan.add(new Forum("Science3", 3));

            var obj = DBforumMan.getObj(4);
            obj.CreateSubForum("Politic");
            obj.CreateSubForum("Economy");
            obj.CreateSubForum("Weather");

            var obj2 = DBforumMan.getObj(1);
            obj2.CreateSubForum("Soccer");
            obj2.CreateSubForum("Basketball");
            obj2.CreateSubForum("Weather");

            DBforumMan.update();
             */
        }

        public static ForumManager getInstance()
        {
            if (instance == null)
                return new ForumManager();
            return instance;
        }


        public int CreateForum(string name)
        {
            //loggerIns.Write(ForumLogger.TYPE_INFO, "Creating Forum with name: " + name);
            //loggerIns.Shutdown();

            if ((name == null) || (name == ""))
                throw new ArgumentException(error_emptyTitle);
            foreach (Forum frm in forums)
                if (frm.CompareName(name) == 0)
                {
                    throw new ArgumentException(error_existTitle);
                }
             forums.Add(new Forum(name, forumIdCounter));
             forumIdCounter++;
             return forumIdCounter - 1;
        }

        public int CreateSubForum(string topic, int forumId)
        {
            if ((topic == null) || (topic == ""))
                throw new ArgumentException(error_emptyTitleSub);
            int ans = -1;
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    ans = frm.CreateSubForum(topic);
            }
            return ans;
        }
        public void RemoveForum(int forumId)
        {
            Forum tmp = null;
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                    tmp = frm;
            forums.Remove(tmp);
        }
        public Boolean RemoveSubForum(int forumId, int subForumId, int callerUserId)
        {
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                    return frm.RemoveSubForum(subForumId, callerUserId);
            return false;
        }
        public void AddAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    frm.AddAdmin(userId);
            }
        }
        public void RemoveAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    frm.RemoveAdmin(userId);
            }
        }
        public Boolean IsAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    return frm.IsAdmin(userId);
            }
            return false;
        }
        public int Register(string username, string password, string mail, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                     return frm.Register(username, password,mail);
            }
            throw new ArgumentException(error_forumID + forumId);
        }
        public void UnRegister(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                {
                    frm.UnRegister(userId);
                    return;
                }
            }
            throw new ArgumentException(error_forumID + forumId);
        }
        public int Login(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                {
                    return frm.Login(username, password);
                }
            }
            throw new ArgumentException(error_forumID + forumId);

        }
        public Boolean Logout(int userId, int forumId)
        {
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                    return frm.Logout(userId);
            throw new ArgumentException(error_forumID + forumId);
        }
        public void SetPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength, int forumId)
        {
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                {
                    frm.SetPolicy(numOfModerators, degreeOfEnsuring, uppercase, lowercase
                        , numbers, symbols, minLength);
                    break;
                }
        }
        public Boolean IsModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    return frm.IsModerator(userId, subForumId);
            }
            throw new ArgumentException(error_forumID + forumId); ;
        }
        public void AddModerator(int userId, int forumId, int subForumId, int callerId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    frm.AddModerator(userId, subForumId, callerId);
            }
        }

        public void RemoveModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    frm.RemoveModerator(userId, subForumId);
            }
        }
        public void SetTopic(string topic, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    frm.SetSubTopic(topic, subForumId);
            }
        }
        public int GetForumId(string name)
        {
            foreach (Forum frm in forums)
            {
                if (frm.CompareName(name) == 0)
                    return frm.ForumID;
            }
            throw new ArgumentException(error_forumID+name);
        }

        public int GetSubForumId(int forumId, string topic)
        {
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                    return frm.GetSubForumId(topic);
            throw new ArgumentException(error_forumID + forumId);
        }

        public Boolean IsValid(string password, int forumId)
        {
            foreach (Forum frm in forums)
                if (frm.ForumID == forumId)
                    return frm.IsValidPassword(password);
            return false;
        }

        public int NumOfForums()
        {
            return forums.Count;
        }

        public List<int> GetForumIds()
        {
            List<int> ans = new List<int>();
            foreach (Forum fr in forums)
                ans.Add(fr.ForumID);
            return ans;

        }

        public List<string> GetForumTopics()
        {
            List<string> ans = new List<string>();
            foreach (Forum fr in forums)
                ans.Add(fr.forumName);
            return ans;
        }

        public string GetForumName(int forumId)
        {
            int forumIndex = GetForumIndex(forumId);
            return forums.ElementAt(forumIndex).forumName;                
        }

        public List<int> GetSubForumsIds(int forumId)
        {
            List<int> ans = new List<int>();
            int forumIndex = GetForumIndex(forumId);
            Forum cur = forums.ElementAt(forumIndex);
            List<SubForum> subForums = cur.subForums;
            foreach (SubForum sf in subForums)
            {
                ans.Add(sf.SubForumId);
            }
            return ans;

        }

        public List<string> GetSubForumsTopics(int forumId)
        {
            List<string> ans = new List<string>();
            int forumIndex = GetForumIndex(forumId);
            Forum cur = forums.ElementAt(forumIndex);
            List<SubForum> subForums = cur.subForums;
            foreach (SubForum sf in subForums)
            {
                ans.Add(sf.Topic);
            }
            return ans;
        }


        public Boolean isRegisteredUser(int forumId , int userId)
        {
            List<int> ans = new List<int>();
            int forumIndex = GetForumIndex(forumId);
            Forum cur = forums.ElementAt(forumIndex);
            return cur.isUserRegistered(userId);
        }

        public string GetSubForumTopic(int forumId, int subForumId)
        {
            string ans = null;
            int forumIndex = GetForumIndex(forumId);
            Forum fr = forums.ElementAt(forumIndex);
            ans = fr.GetSubForumTopic(forumId,subForumId);
            return ans;
        }
        public List<int> GetAllComments(int forumId, int subForumId,int firstMessageId)
        {
            List<int> ans = new List<int>();
            int forumIndex = GetForumIndex(forumId);
            Forum cur = forums.ElementAt(forumIndex);

            return ans;
        }

        public string GetUserType(int forumId, int userId)
        {
            if (IsAdmin(userId, forumId))
                return "admin";
            if (isRegisteredUser(forumId, userId))
                return "member";
            return "";
        }

        public string GetUsername(int forumId, int userId)
        {
            Forum fr = GetForum(forumId);
            string ans = fr.GetUserName(userId);
            return ans;
        }

        public List<string> GetSessionHistory(int requesterId, int forumId, int userIdSession)
        {          
            if (IsAdmin(requesterId, forumId))
            {
                Forum fr = GetForum(forumId);
                List<string> ans = fr.GetSessionHistory(userIdSession);
                return ans;
            }
            else
                throw new UnauthorizedAccessException(error_accessDenied);

        }




        private Forum GetForum(int forumId)
        {
            for (int i = 0; i < forums.Count; i++)
            {
                if (forums.ElementAt(i).ForumID == forumId)
                    return forums.ElementAt(i);
            }
            throw new ArgumentException(error_forumID + forumId);
        }

        private int GetForumIndex(int forumId)
        {
            for (int i = 0; i < forums.Count; i++)
            {
                if (forums.ElementAt(i).ForumID == forumId)
                    return i;
            }
            throw new ArgumentException(error_forumID + forumId);
        }


    }
}