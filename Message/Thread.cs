using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public class Thread
    {
        private int forumId;
        private int subForumId;
        private FirstMessage firstMessage;


        public Thread(int forumId, int subForumId, int messageId, int publisherID, string publisherName, string title, string body)
        {
            this.forumId = forumId;
            this.subForumId = subForumId;
            firstMessage = new FirstMessage(messageId, publisherID, publisherName, title, body);
        }

        public int ForumId
        {
            get { return forumId; }
        }

        public int SubForumId
        {
            get { return subForumId; }
        }

        public FirstMessage FirstMessage
        {
            get { return firstMessage; }

        }

        public Message GetMessage()
        {
            return firstMessage;
        }
     

        internal int NumOfMessages(int forumId, int subForumId)
        {
            if ((this.forumId == forumId) && (this.subForumId == subForumId))
                return 1+firstMessage.getNumofComments();
            return 0;
        }

        internal int NumOfMessages(int forumId, int subForumId, int userId)
        {
            if ((this.forumId == forumId) && (this.subForumId == subForumId))
                return firstMessage.NumOfMessagesByUserId(userId);
            return 0;
        }
    }
}
