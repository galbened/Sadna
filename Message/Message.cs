using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    public abstract class Message : IMessage
    {
        public int id { get; set; }
        public int publisherID { get; set; }
        public string publisherName { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime publishDate { get; set; }

        //private int id;
        //private int publisherID;
        //private string publisherName;
        //private string title;
        //private string body;
        //private DateTime publishDate;
        
        public Message()
        {

        }

        public Message(int id, int publisherID, string publisherName, string title, string body)
        {
            this.id = id;
            this.publisherID = publisherID;
            this.publisherName = publisherName;
            this.title = title;
            this.body = body;
            this.publishDate = DateTime.Now;
        }

        public abstract bool isFirst();

        public int MessageID
        {
            get { return id; }
        }

        public String Content
        {
            get { return body; }
        }

        public int PublisherID
        {
            get { return publisherID; }
        }

        public string PublisherName
        {
            get { return publisherName; }
        }

        public string Title
        {
            get { return title; }
        }

        public DateTime PublishDate
        {
            get { return publishDate; }
        }

        public void editMessage(string title, string body)
        {
            this.title = title;
            this.body = body;
        }

    }
}
