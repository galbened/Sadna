using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class FirstMessage : Message
    {

        private HashSet<ResponseMessage> responseMessages = new HashSet<ResponseMessage>();

        public FirstMessage(int messageId, int publisherID, string title, string body, DateTime publishDate)
            : base(messageId, publisherID, title, body, publishDate) { }

        public override bool isFirst()
        {
            return true;
        }


        public bool hasComments()
        {
            return (responseMessages.Count != 0);
        }


        public int getNumofComments()
        {
            return responseMessages.Count;
        }


        public void addResponse(ResponseMessage responseMessage)
        {
            responseMessages.Add(responseMessage);
        }


        public HashSet<ResponseMessage> getResponseMessages()
        {
            return responseMessages;
        }



    }
}
