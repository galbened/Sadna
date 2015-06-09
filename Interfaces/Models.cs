using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public struct ThreadInfo
    {
        public int id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public string publisher { get; set; }
        public List<CommentInfo> comments { get; set; }
    }

    public struct CommentInfo
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public string publisher { get; set; }
    }


    class Models
    {
    }
}
