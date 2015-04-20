using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;


namespace Forum
{
    class ForumManager : IForumManager
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
            forums.Add(new Forum(name,forumIdCounter));
            forumIdCounter++;
            return forumIdCounter - 1;
        }

        public int createSubForum(string topic, int forumId)
        {
            int ans = -1;
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                   ans =  frm.createSubForum(topic);
            }
            return ans;
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
        public void register(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.register(username, password);
            }
        }
        public void login(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.login(username, password);
            }
        }
        public void setPolicy(int numOfModerators, string degreeOfEnsuring, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.setPolicy(numOfModerators, degreeOfEnsuring);
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
    }
}
