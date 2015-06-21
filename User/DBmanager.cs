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

        public DBmanager()
        {
            InitMode();
            if (UseDB())
                 db = new Context();
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
                return db.Members.Find(ID);
            else
                return null;
        }

        public List<Member> getAll()
        {
            if (UseDB())
                return db.Members.ToList();
            else
                return new List<Member>();
        }

        public void update()
        {
            if (UseDB())
                 db.SaveChanges();
        }


        public void add(Member obj)
        {
            if (UseDB())
                 db.Members.Add(obj);
        }


        public void remove(Member obj)
        {
            if (UseDB())
                 db.Members.Remove(obj);
        }
    }
}

