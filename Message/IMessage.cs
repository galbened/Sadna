using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public interface IMessage
    {
        public String getContent();
        public String getPublisherName();
        public DateTime getPublishDate();
    }
}
