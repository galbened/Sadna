using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    public class Message : IMessage
    {
        String title;
        String body;
        DateTime publishDate;
        int publisherId;
        //User publisher;

        public Message()
        {

        }

        public String getContent()
        {
            return body;
        }

        public String getPublisherName()
        {
            return "";
        }

        public DateTime getPublishDate()
        {
            return publishDate;
        }
    }
}
