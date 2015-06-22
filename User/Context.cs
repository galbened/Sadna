using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class Context : DbContext
    {
        public Context()
            : base("DefaultConnection") // <-- this is what i added.
        {
        }
        public DbSet<Member> Members { get; set; }
        //public DbSet<Password> Passwords { get; set; }
    }
}
