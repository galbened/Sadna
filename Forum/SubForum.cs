using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumsSystem
{
    public class SubForum
    {
        private string topic;
        private List<int> moderatorsId;

        public SubForum(string topic)
        {
            moderatorsId = new List<int>();
            this.topic = topic;
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

        public string getTopic()
        {
            return this.topic;
        }

        public void changeTopic(string topic)
        {
            this.topic = topic;
        }
    }

}
