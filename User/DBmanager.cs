using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Configuration;

namespace User
{
    class DBmanager : IDBManager<Member>
    {
        Context db;
        private int mode;
        private readonly object _locker = new object();
        public Dictionary<int,Member> Members;

        public DBmanager()
        {
            InitMode();
            if (UseDB())
            {
                db = new Context();
            }
            else
            {
                Members = new Dictionary<int, Member>();
            }
            //db.Database.ExecuteSqlCommand("DELETE FROM Members DBCC CHECKIDENT ('Members',RESEED, 0)");
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
        
        public Member getObj(int ID)
        {
            if (UseDB())
                lock (_locker)
                {
                    return db.Members.Find(ID);
                }
            else
            {
                return Members[ID];
            }
                //return null;
        }

        public List<Member> getAll()
        {
            if (UseDB())
                lock (_locker)
                {
                    return db.Members.ToList();
                }
            else
                return Members.Values.ToList();
        }

        public void update()
        {
            if (UseDB())
                lock (_locker)
                {
                    db.SaveChanges();
                }
        }


        public void add(Member obj)
        {
            if (UseDB())
                lock (_locker)
                {
                    db.Members.Add(obj);
                }
            else
                Members.Add(obj.memberID, obj);
        }


        public void remove(Member obj)
        {
            if (UseDB())
                lock (_locker)
                {
                    db.Members.Remove(obj);
                }
            else
                Members.Remove(obj.memberID);
        }
    }
}

