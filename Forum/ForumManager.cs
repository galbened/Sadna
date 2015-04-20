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

        void createForum(string name)
        {
            forums.Add(new Forum(name,forumIdCounter));
            forumIdCounter++;
        }

        void createSubForum(string topic, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.createSubForum(topic);
            }
        }
        void addAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.addAdmin(userId);
            }
        }
        void removeAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.removeAdmin(userId);
            }
        }
        Boolean isAdmin(int userId, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    return frm.isAdmin(userId);
            }
            return false;
        }
        void register(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.register(username, password);
            }
        }
        void login(string username, string password, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.login(username, password);
            }
        }
        void setPolicy(int numOfModerators, string degreeOfEnsuring, int forumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.setPolicy(numOfModerators, degreeOfEnsuring);
            }
        }
        Boolean isModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    return frm.isModerator(userId, subForumId);
            }
            return false;
        }
        void addModerator(int userId,  int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.addModerator(userId, subForumId);
            }

        }

        void removeModerator(int userId, int forumId, int subForumId)
        {
            foreach (Forum frm in forums)
            {
                if (frm.getId() == forumId)
                    frm.removeModerator(userId, subForumId);
            }
        }
        void setTopic(string topic, int subFrumId);
    }
}
