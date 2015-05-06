﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    public abstract class Message : IMessage
    {
        private int id;
        private int publisherID;
        private string title;
        private string body;
        private DateTime publishDate;
        


        public Message(int id, int publisherID, string title, string body)
        {
            this.id = id;
            this.publisherID = publisherID;
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
