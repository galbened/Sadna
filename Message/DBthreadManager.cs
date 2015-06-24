using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Configuration;

namespace Message
{
    class DBthreadManager : IDBManager<Thread>
    {
        Context db;
        private int mode;
        public List<Thread> Threads;

        public DBthreadManager()
        {
            InitMode();
            if (UseDB())
                db = new Context();
            else
                Threads = new List<Thread>();
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members");
        }

        private void InitMode()
        {
            string modeTxt = ConfigurationManager.AppSettings["mode"];
            if (modeTxt.CompareTo("NoDB") == 0)
                mode = 0;
            else
                mode = 1;
        }

        private bool UseDB()
        {
            if (mode == 0)
                return false;
            else
                return true;
        }

        public Thread getObj(int ID)
        {
            if (UseDB())
                return db.Threads.Find(ID);
            else
                return null;
        }

        public List<Thread> getAll()
        {
            if (UseDB())
                return db.Threads.ToList();
            else
                return Threads;
        }

        public void update()
        {
            if (UseDB())
                db.SaveChanges();
        }


        public void add(Thread obj)
        {
            if (UseDB())
                db.Threads.Add(obj);
            else
                Threads.Add(obj);
        }


        public void remove(Thread obj)
        {
            if (UseDB())
                db.Threads.Remove(obj);
            else
                Threads.Remove(obj);
        }
    }
}
