using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class Thread
    {
        NewMessage newMessage;
        int subForumId;

        public Thread(int subForumId, int publisherID, string title, string body, DateTime publishDate)
        {
            this.subForumId = subForumId;
            newMessage = new NewMessage(publisherID, title, body, publishDate);
        }
    }
}
