using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessageManager
    {
        /*
         * Creating new Thread
         * int subForumId - ID of subForum the thread published in
         * int publisherID - ID of publisher (User)
         * return value - ID of the created Thread
         **/
        int addThread(int subForumId, int publisherID, string title, string body);

        /*
         * Add comment (ResponseMessage)
         * int firstMessageId - the head message of the thread
         * int publisherID - ID of publisher (User)
         * return value - ID of the created Comment
         **/
        int addComment(int firstMessageId, int publisherID, string title, string body);

        /*
         * Edit Message (First\Response)
         * int messageId - ID of the message to edit
         * the user permission to edit the message should be checked
         **/
        void editMessage(int messageId, string title, string body);

        /*
         * Delete Message(First\Response)
         * int messageId - ID of the message to delete
         * the user permission to delete the message should be checked 
         **/
        void deleteMessage(int messageId);
    }
}
