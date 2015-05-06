using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class Thread
    {
        FirstMessage firstMessage;
        int forumId;
        int subForumId;

        public Thread(int forumId, int subForumId, int messageId, int publisherID, string title, string body)
        {
            this.forumId = forumId;
            this.subForumId = subForumId;
            firstMessage = new FirstMessage(messageId, publisherID, title, body);
        }
     

        public FirstMessage getMessage()
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
