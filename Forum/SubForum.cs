using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum
{
    public class SubForum
    {
        public int subForumId { get; set; }
        private string topic;
        public List<Moderator> moderators { get; set; }
        //private List<Moderator> moderators;
        //private int subForumId;

        public SubForum() { }

        public SubForum(string topic, int id)
        {
            moderators = new List<Moderator>();
            moderators.Add(new Moderator(1, 1));
            this.topic = topic;
            subForumId = id;
        }
        internal Boolean Contains(int id)
        {
            foreach (Moderator mod in moderators)
            {
                if (mod.compareId(id))
                    return true;
            }
            return false;
        }

        public Boolean IsModerator(int userId)
        {
            return Contains(userId);
        }

        public List<int> GetModeratorsIds()
        {
            List<int> ans = new List<int>();
            foreach (Moderator md in moderators)
            {
                ans.Add(md.GetModeratorId());
            }
            return ans;
        }
        
        public void AddModerator(int userId, int callerId)
        {
            if (!(Contains(userId)))
                moderators.Add(new Moderator(userId, callerId));
        }

         public void RemoveModerator(int userId)
        {
            Remove(userId);
        }

         internal void Remove(int userId)
         {
             foreach (Moderator mod in moderators)
             {
                 if (mod.compareId(userId))
                 {
                     moderators.Remove(mod);
                     break;
                 }
             }
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

        public int NumOfModerators()
        {
            return moderators.Count;
        }
    }

}
