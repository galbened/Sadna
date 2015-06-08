using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    interface IMessage
    {
        int getMessageID();
        bool isFirst();
        string getContent();
        string getPublisherName();
        DateTime getPublishDate();
        void editMessage(string title, string body); 
    }
}
