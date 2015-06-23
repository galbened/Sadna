using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    [Table("FirstMessages")]
    public class FirstMessage : Message
    {
        private HashSet<ResponseMessage> responseMessages = new HashSet<ResponseMessage>();

        public FirstMessage() : base()
        {

        }

        public FirstMessage(int messageId, int publisherID,string publisherName, string title, string body)
            : base(messageId, publisherID, publisherName, title, body) { }

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

        public void removeRespone(ResponseMessage responseMessage)
        {
            responseMessages.Remove(responseMessage);
        }

        public bool IncludeComment(ResponseMessage rm)
        {
            if (responseMessages.Contains(rm))
                return true;
            return false;
        }


        public HashSet<ResponseMessage> ResponseMessages
        {
            get { return responseMessages; }
        }

        internal int NumOfMessagesByUserId(int userId)
        {
            int ans = 0;
            if (PublisherID == userId)
                ans++;
            foreach (ResponseMessage respMsg in responseMessages)
                if (respMsg.PublisherID == userId)
                    ans++;
            return ans;
        }
    }
}
