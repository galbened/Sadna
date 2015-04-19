using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class NewMessage : Message
    {

        private List<ResponseMessage> responseMessages = new List<ResponseMessage>();

        public NewMessage(int publisherID, string title, string body, DateTime publishDate)
            : base(publisherID, title, body, publishDate) { }
 

        public bool hasComments()
        {
            return (responseMessages.Count != 0);
        }

        public int getNumofComments()
        {
            return responseMessages.Count;
        }

        public void addComment(ResponseMessage responseMessage)
        {
            responseMessages.Add(responseMessage);
        }



    }
}
