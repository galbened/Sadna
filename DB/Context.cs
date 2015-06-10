using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;
using Forum;
using Message;

namespace DB
{
    class Context : DbContext
    {
        public DbSet<User.Member> Members { get; set; }
        public DbSet<Forum.Forum> Forums { get; set; }
        public DbSet<SubForum> SubForums { get; set; }
        public DbSet<Message.Message> Messages { get; set; }
        public DbSet<Message.Thread> Threads { get; set; }
    }
}
