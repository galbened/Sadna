using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interfaces
{
    interface IForumManager
    {
        int createForum(string name, int policyId);
        int createSubForum(string topic, int forumId);
        void addAdmin(int userId, int forumId);
        void removeAdmin(int userId, int forumId);
        Boolean isAdmin(int userId, int forumId);
        void register(string username, string password, int forumId);
        void login(string username, string password, int forumId);
        void setPolicy(int numOfModerators, string degreeOfEnsuring, int forumId);
        Boolean isModerator(int userId, int forumId, int subForumId);
        void addModerator(int userId, int subForumId);
        void removeModerator(int userId, int subForumId);
        void setTopic(string topic, int subForumId);
    }
}

