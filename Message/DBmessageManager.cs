using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Message
{
    class DBmessageManager : IDBManager<Message>
    {
        Context db;

        public DBmessageManager()
        {
            db = new Context();
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members");
        }

        public Message getObj(int ID)
        {
            return db.Messages.Find(ID);
        }

        public List<Message> getAll()
        {
            return db.Messages.ToList();
        }

        public void update()
        {
            db.SaveChanges();
        }


        public void add(Message obj)
        {
            db.Messages.Add(obj);
        }


        public void remove(Message obj)
        {
            db.Messages.Remove(obj);
        }
    }
}

