using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using ForumLoggers;

namespace Message
{
    public class MessageManager : IMessageManager
    {
        private HashSet<Message> messages;
        private HashSet<Thread> threads;
        private static MessageManager instance = null;
        private int lastMessageID;
        private const string error_emptyTitle = "Cannot add thread without title";
        private const string error_messageIdNotFound = "Message ID doesn't exist";
        private const string error_callerIDnotMatch = "Only publisher can edit message";
        private const string error_wrongForumOrSubForumId = "ForumId or SubForumId not found in all Threads";
        private ForumLogger _logger;

        IDBManager<Message> DBmessageMan;

        public static IMessageManager Instance()
        {
            if (instance == null)
                return new MessageManager();
            return instance;
        }

        private MessageManager()
        {
            messages = new HashSet<Message>();
            threads = new HashSet<Thread>();
            lastMessageID = -1;
            _logger = ForumLogger.GetInstance();

            DBmessageMan = new DBmessageManager();

            /*
            DBmessageMan.add(new FirstMessage(1, 1, "Gal", "Test", "Checking messages DB!"));
            var bla = new FirstMessage(1, 1, "Gal", "Test2", "Checking messages DB! 2");
            DBmessageMan.add(bla);
            DBmessageMan.add(new ResponseMessage(bla,1,1,"Tomer","Very important title","bla bla bla bla"));

            DBmessageMan.update();

            var obj = DBmessageMan.getObj(11);

             */
        }


        public int addThread(int forumId, int subForumId, int publisherId, string publisherName, string title, string body)
        {
            if ((title == null) || (title.Equals("")))
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Failed adding new thread " + error_emptyTitle);
                throw new ArgumentException(error_emptyTitle);
            }
            lastMessageID++;
            int messageId = lastMessageID;
            Thread thread = new Thread(forumId, subForumId, messageId, publisherId,publisherName, title, body);
            threads.Add(thread);
            Message ms = thread.GetMessage();
            messages.Add(ms);
            _logger.Write(ForumLogger.TYPE_INFO, "New thread was added " + ms.MessageID);
            return ms.MessageID;
        }


        public int addComment(int firstMessageID, int publisherID, string publisherName, string title, string body)
        {
            if ((title == null) || (title.Equals("")))
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Failed adding new comment " + error_emptyTitle);
                throw new ArgumentException(error_emptyTitle);
            }
            FirstMessage first = (FirstMessage)findMessage(firstMessageID);
            //checking if firstMessageID exists and really FirstMessage
            if ((first != null) && (first.isFirst()))
            {
                lastMessageID++;
                int messageId = lastMessageID;
                ResponseMessage rm = new ResponseMessage((FirstMessage)first, messageId, publisherID, publisherName, title, body);
                first.addResponse(rm);
                messages.Add(rm);
                _logger.Write(ForumLogger.TYPE_INFO, "New comment was added " + rm.MessageID);
                return rm.MessageID;
            }
            _logger.Write(ForumLogger.TYPE_ERROR, "Failed adding new comment " + error_messageIdNotFound);
            throw new InvalidOperationException(error_messageIdNotFound);
        }


        public bool editMessage(int messageId, string title, string body, int callerID)
        {
            if ((title == null) || (title.Equals("")))
            {
                _logger.Write(ForumLogger.TYPE_ERROR, "Failed editing message " + messageId + ": " +error_emptyTitle);
                throw new ArgumentException(error_emptyTitle);
            }
            Message ms = findMessage(messageId);
            if (ms != null)
            {
                if (ms.PublisherID == callerID)
                {
                    ms.editMessage(title, body);
                    _logger.Write(ForumLogger.TYPE_INFO, "Message was edited " + messageId);
                    return true;
                }
                else
                {
                    _logger.Write(ForumLogger.TYPE_ERROR, "Failed editing message " + messageId + ": " + error_callerIDnotMatch + ". callerID is " + callerID);
                    throw new ArgumentException(error_callerIDnotMatch);
                }
            }
            //_logger.Write(ForumLogger.TYPE_ERROR, "Failed editing message " + messageId + ": " + error_messageIdNotFound);
            throw new InvalidOperationException(error_messageIdNotFound);
        }



        public bool deleteMessage(int messageID)
        {
            Message ms = findMessage(messageID);
            if (ms != null)
            {
                // if firstMessage, it should be deleted with all its comments
                if (ms.isFirst())
                {
                    HashSet<ResponseMessage> messagesForDeletion = ((FirstMessage)ms).ResponseMessages;
                    foreach (ResponseMessage rm in messagesForDeletion)
                        messages.Remove(rm);
                }
                //remove the message anyway
                 messages.Remove(ms);
                 _logger.Write(ForumLogger.TYPE_INFO, "Message was deleted " + messageID);
                 return true;
            }
            _logger.Write(ForumLogger.TYPE_ERROR, "Failed deleting message " + messageID + ": " + error_messageIdNotFound);
            throw new InvalidOperationException(error_messageIdNotFound);
                

        }

        private Message findMessage(int messageID)
        {
            bool found = false;
            int messagesSearched = 0;
            while ((!found) && (messagesSearched < messages.Count))
            {
                Message cur = messages.ElementAt<Message>(messagesSearched);
                if (cur.MessageID == messageID)
                {
                    found = true;
                    return cur;
                }
                messagesSearched++;
            }

            //if messageID is wrong
            return null;
        }

        public int NumOfMessages(int forumId, int subForumId)
        {
            int ans = 0;
            foreach (Thread thread in threads)
            {
                ans += thread.NumOfMessages(forumId, subForumId);
            }
            return ans;
        }

        public int NumOfMessages(int forumId, int subForumId, int userId)
        {
            int ans = 0;
            foreach (Thread thread in threads)
            {
                ans += thread.NumOfMessages(forumId, subForumId, userId);
            }
            return ans;
        }

        public List<ThreadInfo> GetAllThreads(int forumId, int subForumId)
        {
            List<ThreadInfo> ans = new List<ThreadInfo>();
            Thread thread = null;
            for (int i=0; i < threads.Count; i++)
            {
                thread = threads.ElementAt(i);
                if (thread.ForumId == forumId && thread.SubForumId == subForumId)
                {
                    ThreadInfo cur = new ThreadInfo();
                    cur.id = thread.FirstMessage.MessageID;
                    cur.topic = thread.FirstMessage.Title;
                    cur.content = thread.FirstMessage.Content;
                    cur.date = thread.FirstMessage.PublishDate;
                    cur.publisher = thread.FirstMessage.PublisherName;
                    cur.comments = GetAllThreadComments(cur.id);
                    ans.Add(cur);
                }
                                   
            }
            return ans;
                
        }

        private List<CommentInfo> GetAllThreadComments(int firstMessageId)
        {
            List<CommentInfo> ans = new List<CommentInfo>();
            CommentInfo cur = new CommentInfo();
            foreach (Message ms in messages)
            {
                if (!ms.isFirst())
                {
                    ResponseMessage rm = (ResponseMessage)ms;
                    if (rm.FirstMessage.MessageID == firstMessageId)
                    {
                        cur.Id = rm.MessageID;
                        cur.topic = rm.Title;
                        cur.content = rm.Content;
                        cur.date = rm.PublishDate;
                        cur.publisher = rm.PublisherName;
                        ans.Add(cur);
                    }
                }
            }
            return ans;
        }

        

        

    }
}
