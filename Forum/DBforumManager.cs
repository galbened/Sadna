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
    class DBforumManager : IDBManager<Forum>
    {
        Context db;
        private int mode;
        public Dictionary<int, Forum> Forums;

        public DBforumManager()
        {
            InitMode();
            if (UseDB())
            {
                db = new Context();
                db.Configuration.LazyLoadingEnabled = false;
            }
            else
            {
                Forums = new Dictionary<int, Forum>();
            }
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE SubForums");
            //db.Database.ExecuteSqlCommand("DELETE FROM Fora DBCC CHECKIDENT ('Fora',RESEED, 0)");
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

        public Forum getObj(int ID)
        {

            if (UseDB())
                return db.Forums.Find(ID);
            else
                return Forums[ID];
            //return db.Forums.SingleOrDefault(p => p.forumID == ID);
        }

        public List<Forum> getAll()
        {
            if (UseDB())
                return db.Forums.ToList();
            else
                return Forums.Values.ToList();
        }

        public void update()
        {
            if (UseDB())
                db.SaveChanges();
        }


        public void add(Forum obj)
        {
            if (UseDB())
              db.Forums.Add(obj);
            else
                Forums.Add(obj.forumID, obj);
        }


        public void remove(Forum obj)
        {
            if (UseDB())
              db.Forums.Remove(obj);
            else
                Forums.Remove(obj.forumID);
        }
    }
}

