using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    class MessageManager : IMessageManager
    {
        private HashSet<Message> messages;


        public MessageManager()
        {
            messages = new HashSet<Message>();
        }


        public void addThread(int subForumId, int publisherId, string title, string body, DateTime publishDate)
        {
            int messageId = messages.Count;
            Thread thread = new Thread(subForumId, messageId, publisherId, title, body, publishDate);
            Message ms = thread.getMessage();
            messages.Add(ms);
        }


        public void addComment(int firstMessageID, int publisherID, string title, string body, DateTime publishDate)
        {
            FirstMessage first = (FirstMessage)findMessage(firstMessageID);
            //checking if firstMessageID exists and really FirstMessage
            if ((first != null) && (first.isFirst()))
            {
                int messageId = messages.Count;
                ResponseMessage rm = new ResponseMessage((FirstMessage)first, messageId, publisherID, title, body, publishDate);
                first.addResponse(rm);
                messages.Add(rm);
            }
        }


        public void editMessage(int messageId, int userId, string title, string body)
        {

            //check if the user is able to edit specific message


            Message ms = findMessage(messageId);
            if (ms != null)
                ms.editMessage(title, body);
        }



        public void deleteMessage(int messageID, int userId)
        {

            //check if the user is able to delete specific message


            Message ms = findMessage(messageID);
            if (ms != null)
            {
                // if firstMessage, it should be deleted with all its comments
                if (ms.isFirst())
                {
                    HashSet<ResponseMessage> messagesForDeletion = ((FirstMessage)ms).getResponseMessages();
                    foreach (ResponseMessage rm in messagesForDeletion)
                        messages.Remove(rm);
                }
                //remove the message anyway
                 messages.Remove(ms);
            }
                

        }

        private Message findMessage(int messageID)
        {
            bool found = false;
            int messagesSearched = 0;
            while ((!found) && (messagesSearched < messages.Count))
            {
                Message cur = messages.ElementAt<Message>(messagesSearched);
                if (cur.getMessageID() == messageID)
                {
                    found = true;
                    return cur;
                }
                messagesSearched++;
            }

            //if messageID is wrong
            return null;
        }

        

        

    }
}
