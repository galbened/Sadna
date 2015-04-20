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
         * The implementation is singelton
         * return value - instance of IMessageManager
         **/
        public IMessageManager Instance();

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
         * return value - ID of the created Comment. return -1 if firstMessageId doesn't exist
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
