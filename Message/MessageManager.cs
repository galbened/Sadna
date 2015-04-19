using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    class MessageManager : IMessageManager
    {
        private HashSet<NewMessage> messages;


        public MessageManager()
        {
            messages = new HashSet<NewMessage>();
        }


        public void addThread(int subForumId, int publisherID, string title, string body, DateTime publishDate)
        {
            Thread thread = new Thread(subForumId, publisherID, title, body, publishDate);
        }


        public void addComment(int firstMessageID, int publisherID, string title, string body, DateTime publishDate)
        {

        }


        public void deleteMessage(int messageID)
        {

        }

        

    }
}
