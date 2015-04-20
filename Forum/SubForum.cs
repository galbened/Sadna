using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum
{
    public class SubForum
    {
        private string topic;
        private List<int> moderatorsId;
        private int subForumId;

        public SubForum(string topic, int id)
        {
            moderatorsId = new List<int>();
            this.topic = topic;
            subForumId = id;
        }

        public Boolean isModerator(int userId)
        {
            return moderatorsId.Contains(userId);
        }
        
        public void addModerator(int userId)
        {
            if (!(moderatorsId.Contains(userId)))
                moderatorsId.Add(userId);
        }

         public void removeModerator(int userId)
        {
            moderatorsId.Remove(userId);
        }

        public string getTopic()
        {
            return this.topic;
        }

        public void setTopic(string topic)
        {
            this.topic = topic;
        }

        internal int getId()
        {
            return subForumId;
        }
    }

}
