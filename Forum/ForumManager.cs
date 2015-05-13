using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using ForumLoggers;


namespace Forum
{
    public class ForumManager : IForumManager
    {
        private List<Forum> forums;
        private static ForumManager instance = null;
        private static int forumIdCounter;
        private const string error_emptyTitle = "Cannot create forum without title";
        private const string error_emptyTitleSub = "Cannot create subForum without topic";
        private const string error_existTitle = "Cannot create forum with already exit title";
        private const string error_forumID = "No such forum: ";
        //private ForumLogger loggerIns;

        private ForumManager()
        {
            forums = new List<Forum>();
            forumIdCounter = 1000;
            //loggerIns = ForumLogger.GetInstance();
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

        public int CreateSubForum(string topic, int forumId, int callerUserId)
        {
            if ((topic == null) || (topic == ""))
                throw new ArgumentException(error_emptyTitleSub);
            int ans = -1;
            foreach (Forum frm in forums)
            {
                if (frm.ForumID == forumId)
                    ans = frm.CreateSubForum(topic, callerUserId);
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
    }
}