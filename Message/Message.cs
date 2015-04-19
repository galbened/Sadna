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
        private int publisherID;
        private string title;
        private string body;
        private DateTime publishDate;
        


        public Message(int publisherID, string title, string body, DateTime publishDate)
        {
            this.publisherID = publisherID;
            this.title = title;
            this.body = body;
            this.publishDate = publishDate;
        }

        public String getContent()
        {
            return body;
        }

        public string getPublisherName()
        {
            return "";
        }

        public DateTime getPublishDate()
        {
            return publishDate;
        }
    }
}
