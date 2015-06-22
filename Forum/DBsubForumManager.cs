using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Configuration;

namespace Forum
{
    class DBsubForumManager : IDBManager<SubForum>
    {
        Context db;
        private int mode;

        public DBsubForumManager()
        {
            InitMode();
            if (UseDB())
                db = new Context();
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

        public SubForum getObj(int ID)
        {
            if (UseDB())
                return db.SubForums.Find(ID);
            else
                return null;
        }

        public List<SubForum> getAll()
        {
            if (UseDB())
                return db.SubForums.ToList();
            else
                return new List<SubForum>();
        }

        public void update()
        {
            if (UseDB())
                db.SaveChanges();
        }


        public void add(SubForum obj)
        {
            if (UseDB())
             db.SubForums.Add(obj);
        }


        public void remove(SubForum obj)
        {
            if (UseDB())
              db.SubForums.Remove(obj);
        }
    }
}

