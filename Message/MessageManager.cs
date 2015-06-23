using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace Message
{
    public class MessageManager : IMessageManager
    {
        private HashSet<Message> messages;
        //private HashSet<Thread> threads;
        private static MessageManager instance = null;
        private int lastMessageID;
        private const string error_emptyTitle = "Cannot add thread without title";
        private const string error_messageIdNotFound = "Message ID doesn't exist";
        private const string error_callerIDnotMatch = "Only publisher can edit message";
        private const string error_wrongForumOrSubForumId = "ForumId or SubForumId not found in all Threads";
        private const string error_responseNotThread = "ThreadId expected but got comment message Id";
        private const string error_wrongWords = "The message contains wrong words";

        //IDBManager<Message> DBmessageMan;
        IDBManager<Thread> DBthreadMan;

        private HashSet<string> badWords;

        public static IMessageManager Instance()
        {
            if (instance == null)
                return new MessageManager();
            return instance;
        }

        private MessageManager()
        {
            messages = new HashSet<Message>();
            //threads = new HashSet<Thread>();
            lastMessageID = -1;

            InitWrongWords();

            //DBmessageMan = new DBmessageManager();
            DBthreadMan = new DBthreadManager();

            /*
            DBmessageMan.add(new FirstMessage(1, 1, "Gal", "Test", "Checking messages DB!"));
            var bla = new FirstMessage(1, 1, "Gal", "Test2", "Checking messages DB! 2");
            DBmessageMan.add(bla);
            DBmessageMan.add(new ResponseMessage(bla,1,1,"Tomer","Very important title","bla bla bla bla"));

            DBmessageMan.update();

            var obj = DBmessageMan.getObj(11);

             */
        }

       private void InitWrongWords()
       {
           badWords = new HashSet<string>();
           badWords.Add("fuck");
           badWords.Add("sharmuta");
           badWords.Add("sharlila");
           badWords.Add("sharlil");
           badWords.Add("sharmut");
           badWords.Add("motherfucker");
           badWords.Add("nigger");
           badWords.Add("asshole");
           badWords.Add("shirlul");
           badWords.Add("shirluh");
       }

        public int addThread(int forumId, int subForumId, int publisherId, string publisherName, string title, string body)
        {
            if ((title == null) || (title.Equals("")))
            {
                throw new ArgumentException(error_emptyTitle);
            }
            if (!IsValidMessage(title, body))
                throw new ArgumentException(error_wrongWords);
            lastMessageID++;
            int messageId = lastMessageID;
            Thread thread = new Thread(forumId, subForumId, messageId, publisherId,publisherName, title, body);
            DBthreadMan.add(thread);
            //threads.Add(thread);
            Message ms = thread.GetMessage();
            messages.Add(ms);
            saveMessageDB();
            return ms.MessageID;
        }


        public int addComment(int firstMessageID, int publisherID, string publisherName, string title, string body)
        {
            if ((title == null) || (title.Equals("")))
            {
                throw new ArgumentException(error_emptyTitle);
            }
            if (!IsValidMessage(title, body))
                throw new ArgumentException(error_wrongWords);
            FirstMessage first = (FirstMessage)findMessage(firstMessageID);
            //checking if firstMessageID exists and really FirstMessage
            if ((first != null) && (first.isFirst()))
            {
                lastMessageID++;
                int messageId = lastMessageID;
                ResponseMessage rm = new ResponseMessage(messageId, publisherID, publisherName, title, body);
                first.addResponse(rm);
                messages.Add(rm);
                saveMessageDB();
                return rm.MessageID;
            }
            throw new InvalidOperationException(error_messageIdNotFound);
        }


        public bool editMessage(int userRequesterId, int messageId, string title, string body)
        {
            if ((title == null) || (title.Equals("")))
            {
                throw new ArgumentException(error_emptyTitle);
            }
            if (!IsValidMessage(title, body))
                throw new ArgumentException(error_wrongWords);
            Message ms = findMessage(messageId);
            if (ms != null)
            {
                if ((!(ms.publisherID != userRequesterId)) && (!(userRequesterId != 1)))
                    throw new UnauthorizedAccessException("User " + userRequesterId + " has no permissions to edit the message " + messageId);
                if (ms.PublisherID == userRequesterId)
                {
                    ms.editMessage(title, body);
                    saveMessageDB();
                    return true;
                }
                else
                {
                    throw new ArgumentException(error_callerIDnotMatch);
                }
            }
            throw new InvalidOperationException(error_messageIdNotFound);
        }



        public bool deleteMessage(int userRequesterId, int messageID)
        {
            Message ms = findMessage(messageID);
            if (ms != null)
            {
                if ((!(ms.publisherID != userRequesterId)) && (!(userRequesterId != 1)))
                    throw new UnauthorizedAccessException("User " +userRequesterId+" has no permissions to delete the message " + messageID);
                // if firstMessage, it should be deleted with all its comments
                if (ms.isFirst())
                {
                    HashSet<ResponseMessage> messagesForDeletion = ((FirstMessage)ms).ResponseMessages;
                    foreach (ResponseMessage rm in messagesForDeletion)
                        messages.Remove(rm);
                }
                //remove the message anyway
                 messages.Remove(ms);
                 saveMessageDB();
                 return true;
            }
            throw new InvalidOperationException(error_messageIdNotFound);
                

        }

    

        public int NumOfMessages(int forumId, int subForumId)
        {
            int ans = 0;
            foreach (Thread thread in DBthreadMan.getAll())
            {
                ans += thread.NumOfMessages(forumId, subForumId);
            }
            return ans;
        }

        public int NumOfMessages(int forumId, int subForumId, int userId)
        {
            int ans = 0;
            foreach (Thread thread in DBthreadMan.getAll())
            {
                ans += thread.NumOfMessages(forumId, subForumId, userId);
            }
            return ans;
        }

        public List<ThreadInfo> GetAllThreads(int forumId, int subForumId)
        {
            List<ThreadInfo> ans = new List<ThreadInfo>();
            Thread thread = null;
            for (int i = 0; i < DBthreadMan.getAll().Count; i++)
            {
                thread = DBthreadMan.getAll().ElementAt(i);
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

        public int GetNumOfComments(int threadId)
        {
            Message message = findMessage(threadId);
            FirstMessage fm;
            if (message.isFirst())
                fm = (FirstMessage)message;
            else
                throw new ArgumentException(error_responseNotThread);
            return fm.getNumofComments();

        }

       


        /**
         * Useful Private Functions 
         * 
         * 
         * 
         */

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



        public List<CommentInfo> GetAllThreadComments(int firstMessageId)
        {
            List<CommentInfo> ans = new List<CommentInfo>();
            CommentInfo cur = new CommentInfo();
            Message firstMessage = findMessage(firstMessageId);
            if (firstMessage == null)
                return null;
            FirstMessage fm = (FirstMessage)firstMessage;
            if (fm.isFirst())
            {
                foreach (ResponseMessage rm in fm.ResponseMessages)
                {
                    cur.Id = rm.MessageID;
                    cur.topic = rm.Title;
                    cur.content = rm.Content;
                    cur.date = rm.PublishDate;
                    cur.publisher = rm.PublisherName;
                    ans.Add(cur);                                      
                }
            }
            return ans;
        }


        private bool IsValidMessage(string title, string body)
        {
            string[] titleSplit = title.Split(' ');
            foreach (string word in titleSplit)
            {
                if (badWords.Contains(word))
                    return false;
            }
            string[] bodySplit = body.Split(' ');
            foreach (string word in bodySplit)
            {
                if (badWords.Contains(word))
                    return false;
            }
            return true;

        }


        public void saveMessageDB()
        {
            //DBmessageMan.update();
            DBthreadMan.update();
        }
    }
}
