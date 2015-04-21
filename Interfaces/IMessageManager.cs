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
         * int forumId - ID of forum the subForum located in
         * int subForumId - ID of subForum the thread published in
         * int publisherID - ID of publisher (User)
         * return value - ID of the created Thread, -1 if title is empty
         **/
        int addThread(int forumId, int subForumId, int publisherID, string title, string body);

        /*
         * Add comment (ResponseMessage)
         * int firstMessageId - the head message of the thread
         * int publisherID - ID of publisher (User)
         * return value - ID of the created Comment. return -1 if title is empty
         *                                           return -2 if firstMessageId doesn't exist
         **/
        int addComment(int firstMessageId, int publisherID, string title, string body);

        /*
         * Edit Message (First\Response)
         * int messageId - ID of the message to edit
         * return value - true if message edited successfully, else false
         **/
        bool editMessage(int messageId, string title, string body);

        /*
         * Delete Message(First\Response)
         * int messageId - ID of the message to delete
         * return value - true if message deleted successfully, else false
         **/
        bool deleteMessage(int messageId);
    }
}
