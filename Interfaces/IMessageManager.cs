using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMessageManager
    {

        /// <addThread>
        /// Creating new Thread
        /// </addThread>
        /// <param name="forumId">ID of forum the subForum located in</param>
        /// <param name="subForumId">ID of subForum the thread published in</param>
        /// <param name="publisherID">ID of publisher (User)</param>
        /// <param name="publisherName">Name of publisher (User)</param>
        /// <returns>ID of the created Thread</returns>
        /// <throws>ArgumentException if title is empty</throws>
        int addThread(int forumId, int subForumId, int publisherID, string publisherName, string title, string body);

  
        /// <addComment>
        /// Add comment (ResponseMessage)
        /// </addComment>
        /// <param name="firstMessageId">the head message of the thread</param>
        /// <param name="publisherID">ID of publisher (User)</param>
        /// <param name="publisherName">Name of publisher (User)</param>
        /// <returns>ID of the created Comment</returns>
        /// <throws>
        /// ArgumentException if title is empty
        /// InvalidOperationException if firstMessageId doesn't exist
        /// </throws>
        int addComment(int firstMessageId, int publisherID, string publisherName, string title, string body);

    
        /// <editMessage>
        /// Edit Message (First\Response)
        /// </editMessage>
        /// <param name="messageId">ID of the message to edit</param>
        /// <param name="callerID">ID of requesting user</param>
        /// <returns>true if message edited successfully</returns>
        /// <throws>
        /// ArgumentException if title is empty
        /// InvalidOperationException if messageId doesn't exist
        /// </throws>
        bool editMessage(int messageId, string title, string body, int callerID);


        /// <deleteMessage>
        /// Delete Message(First\Response)
        /// </deleteMessage>
        /// <param name="messageId">ID of the message to delete</param>
        /// <returns>true if message deleted successfully</returns>
        /// <throws>InvalidOperationException if messageId doesn't exist</throws>
        bool deleteMessage(int messageId);


        /*
         * return the number of messages writen in specific subforum
         * get the forumId and subForumId 
         * */
        int NumOfMessages(int forumId, int subForumId);
        /*
        * return the number of messages writen in specific subforum by specific user
        * get the forumId, subForumId and userID
        **/
        int NumOfMessages(int forumId, int subForumId, int userId);


        List<ThreadInfo> GetAllThreads(int forumId, int subForumId);

    }
}
