using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace User
{
    class DBmanager : IDBManager<Member>
    {
        Context db;

        public DBmanager()
        {
            db = new Context();
            db.Database.ExecuteSqlCommand("DELETE FROM Members DBCC CHECKIDENT ('Members',RESEED, 0)");
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members");
        }
        
        public Member getObj(int ID)
        {
            return db.Members.Find(ID);
        }

        public List<Member> getAll()
        {
            return db.Members.ToList();
        }

        public void update()
        {
            db.SaveChanges();
        }


        public void add(Member obj)
        {
            db.Members.Add(obj);
        }


        public void remove(Member obj)
        {
            db.Members.Remove(obj);
        }
    }
}

