using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class ResponseMessage
    {
        private NewMessage firstMessage = null;

        public ResponseMessage(NewMessage firstMessage): base()
        {
            this.firstMessage = firstMessage;
        }


    }
}
