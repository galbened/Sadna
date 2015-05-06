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
         * return value - ID of the created Thread
         * Throws ArgumentException if title is empty
         **/
        int addThread(int forumId, int subForumId, int publisherID, string title, string body);

        /*
         * Add comment (ResponseMessage)
         * int firstMessageId - the head message of the thread
         * int publisherID - ID of publisher (User)
         * return value - ID of the created Comment.
         * Throws ArgumentException if title is empty
         * Throws InvalidOperationException if firstMessageId doesn't exist
         **/
        int addComment(int firstMessageId, int publisherID, string title, string body);

        /*
         * Edit Message (First\Response)
         * int messageId - ID of the message to edit
         * return value - true if message edited successfully
         * Throws ArgumentException if title is empty 
         * Throws ArgumentException if callerID not match callerID 
         * Throws InvalidOperationException if messageId doesn't exist
         **/
        bool editMessage(int messageId, string title, string body, int callerID);

        /*
         * Delete Message(First\Response)
         * int messageId - ID of the message to delete
         * return value - true if message deleted successfully
         * Throws InvalidOperationException if messageId doesn't exist
         **/
        bool deleteMessage(int messageId);

        /*
         * return the number of messages writen in specific subforum
         * get the forumId and subForumId 
         * */
        public int NumOfMessages(int forumId, int subForumId);
        /*
        * return the number of messages writen in specific subforum by specific user
        * get the forumId, subForumId and userID
        **/
        public int NumOfMessages(int forumId, int subForumId, int userId);

    }
}
