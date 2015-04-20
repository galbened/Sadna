using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class ResponseMessage : Message
    {
        private FirstMessage firstMessage = null;

        public ResponseMessage(FirstMessage firstMessage, int messageId, int publisherID, string title, string body, DateTime publishDate):
            base(messageId, publisherID, title, body, publishDate)
        {
            this.firstMessage = firstMessage;
        }

        public override bool isFirst()
        {
            return false;
        }
    }
}
