using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Message
{
    class NewMessage : Message
    {

        private List<ResponseMessage> responseMessages = new List<ResponseMessage>(); 

        public NewMessage() : base()
        {
           
        }

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
