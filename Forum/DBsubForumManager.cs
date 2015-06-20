using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Forum
{
    class DBsubForumManager : IDBManager<SubForum>
    {
        Context db;

        public DBsubForumManager()
        {
            db = new Context();
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members");
        }

        public SubForum getObj(int ID)
        {
            return db.SubForums.Find(ID);
        }

        public List<SubForum> getAll()
        {
            return db.SubForums.ToList();
        }

        public void update()
        {
            db.SaveChanges();
        }


        public void add(SubForum obj)
        {
            db.SubForums.Add(obj);
        }


        public void remove(SubForum obj)
        {
            db.SubForums.Remove(obj);
        }
    }
}

