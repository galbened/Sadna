using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    class Context : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Thread> Threads { get; set; }

        public Context()
            : base("name=MessageDBConnectionString")
        { }
    }
}