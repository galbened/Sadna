using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;


namespace Forum
{
    public class ForumManager : IForumManager
    {
        private List<Forum> forums;
        private static ForumManager instance = null;
        private static int forumIdCounter;

        private ForumManager()
        {
            forums = new List<Forum>();
            forumIdCounter = 1000;
        }

        public static ForumManager getInstance()
        {
            if (instance == null)
                return new ForumManager();
            return instance;
        }

        public int createForum(string name)
        {
            foreach (Forum frm in forums)
                if (frm.compareName(name) == 0)
                {
                   return -1;
                }
             forums.Add(new Forum(name, forumIdCounter));
             forumIdCounter++;
             return forumIdCounter - 1;
        }

        public int createSubForum(string topic, int forumId, int callerUserId)
        {
            int ans = -1;
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    ans = frm.createSubForum(topic, callerUserId);
            }
            return ans;
        }
        public void removeForum(int forumId)
        {
            Forum tmp = null;
            foreach (Forum frm in forums)
                if (frm.getId() == forumId)
                    tmp = frm;
            forums.Remove(tmp);
        }
        public void removeSubForum(int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
                if (frm.getId() == forumId)
                    frm.removeSubForum(subForumId);
        }
        public void addAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.addAdmin(userId);
            }
        }
        public void removeAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.removeAdmin(userId);
            }
        }
        public Boolean isAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    return frm.isAdmin(userId);
            }
            return false;
        }
        public int register(string username, string password, string mail, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                     return frm.register(username, password,mail);
            }
            return -1;
        }
        public void unRegister(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                     frm.unRegister(userId);
            }
        }
        public int login(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    return frm.login(username, password);
            } return -1;

        }
        public Boolean logout(int userId, int forumId)
        {
            foreach (Forum frm in forums)
                if (frm.getId() == forumId)
                    return frm.logout(userId);
        }
        public void setPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.setPolicy(numOfModerators, degreeOfEnsuring, uppercase,lowercase
                        ,numbers,symbols,minLength);
            }
        }
        public Boolean isModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    return frm.isModerator(userId, subForumId);
            }
            return false;
        }
        public void addModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.addModerator(userId, subForumId);
            }

        }

        public void removeModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.removeModerator(userId, subForumId);
            }
        }
        public void setTopic(string topic, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.setSubTopic(topic, subForumId);
            }
        }
        public int getForumId(string name)
        {
            foreach (Forum frm in forums)
            {
                if (frm.compareName(name) == 0)
                    return frm.getId();
            }
            return -1;
        }

        public int getSubForumId(int forumId, string topic)
        {
            foreach (Forum frm in forums)
                if (frm.getId() == forumId)
                    return frm.getSubForumId(topic);
            return -1;
        }

        public Boolean isValid(string password, int forumId)
        {
            foreach (Forum frm in forums)
                if (frm.getId() == forumId)
                    return frm.isValidPassword(password);
            return false;
        }
    }
}