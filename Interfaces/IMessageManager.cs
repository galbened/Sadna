using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessageManager
    {
        void addThread(int subForumId, int publisherID, string title, string body, DateTime publishDate);
        void addComment(int firstMessageId, int publisherID, string title, string body, DateTime publishDate);
        //void editMessage(int messageId, int publisherID, string title, string body, DateTime publishDate);     //we don't have requirement for that
        void deleteMessage(int messageId);
    }
}
