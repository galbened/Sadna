using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    [Table("ResponseMessages")]
    public class ResponseMessage : Message
    {

        public ResponseMessage() : base()
        {

        }

        public ResponseMessage(int messageId, int publisherID,string publisherName, string title, string body):
            base(messageId, publisherID, publisherName, title, body)
        {

        }

        public override bool isFirst()
        {
            return false;
        }
    }
}
