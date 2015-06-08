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

        public Boolean IsModerator(int userId)
        {
            return moderatorsId.Contains(userId);
        }
        
        public void AddModerator(int userId)
        {
            if (!(moderatorsId.Contains(userId)))
                moderatorsId.Add(userId);
        }

         public void RemoveModerator(int userId)
        {
            moderatorsId.Remove(userId);
        }

        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }


        internal int SubForumId
        {
            get { return subForumId; }
        }
    }

}
