using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    class DBthreadManager : IDBManager<Thread>
    {
        Context db;

        public DBthreadManager()
        {
            db = new Context();
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members");
        }

        public Thread getObj(int ID)
        {
            return db.Threads.Find(ID);
        }

        public List<Thread> getAll()
        {
            return db.Threads.ToList();
        }

        public void update()
        {
            db.SaveChanges();
        }


        public void add(Thread obj)
        {
            db.Threads.Add(obj);
        }


        public void remove(Thread obj)
        {
            db.Threads.Remove(obj);
        }
    }
}
