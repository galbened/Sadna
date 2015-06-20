using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Forum
{
    class DBforumManager : IDBManager<Forum>
    {
        Context db;

        public DBforumManager()
        {
            db = new Context();
            db.Configuration.LazyLoadingEnabled = false;
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE SubForums");
            //db.Database.ExecuteSqlCommand("DELETE FROM Fora DBCC CHECKIDENT ('Fora',RESEED, 0)");
        }

        public Forum getObj(int ID)
        {
            return db.Forums.Find(ID);
            //return db.Forums.SingleOrDefault(p => p.forumID == ID);
        }

        public List<Forum> getAll()
        {
            return db.Forums.ToList();
        }

        public void update()
        {
            db.SaveChanges();
        }


        public void add(Forum obj)
        {
            db.Forums.Add(obj);
        }


        public void remove(Forum obj)
        {
            db.Forums.Remove(obj);
        }
    }
}

