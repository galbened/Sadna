using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessageManager
    {
        void addThread(int subForumId, int userID, string title, string body);
        void addComment(int firstMessageId, int userID, string title, string body);
        //void editMessage(int messageId, int userID, string title, string body);     //we don't have requirement for that
        void deleteMessage(int messageId);
    }
}
