using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public class ResponseMessage : Message
    {
        private FirstMessage firstMessage = null;

        public ResponseMessage(FirstMessage firstMessage, int messageId, int publisherID,string publisherName, string title, string body):
            base(messageId, publisherID, publisherName, title, body)
        {
            this.firstMessage = firstMessage;
        }

        public override bool isFirst()
        {
            return false;
        }

        public FirstMessage FirstMessage
        {
            get { return firstMessage; }
        }
    }
}
