﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    class Context : DbContext
    {
        public DbSet<Forum> Forums { get; set; }
        public DbSet<SubForum> SubForums { get; set; }

        public Context()
            : base("name=ForumDBConnectionString")
        { }
    }
}