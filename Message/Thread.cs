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
        int subForumId;

        public Thread(int subForumId, int messageId, int publisherID, string title, string body, DateTime publishDate)
        {
            this.subForumId = subForumId;
            firstMessage = new FirstMessage(messageId, publisherID, title, body, publishDate);
        }
     

        public FirstMessage getMessage()
        {
            return firstMessage;
        }
    }
}
