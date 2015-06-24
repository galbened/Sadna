using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Configuration;

namespace Message
{
    class DBmessageManager : IDBManager<Message>
    {
        Context db;
        private int mode;
        public Dictionary<int, Message> Messages;

        public DBmessageManager()
        {
            InitMode();
            if (UseDB())
                db = new Context();
            else
                Messages = new Dictionary<int, Message>();
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

        public Message getObj(int ID)
        {
            if (UseDB())
                return db.Messages.Find(ID);
            else
                return Messages[ID];
        }

        public List<Message> getAll()
        {
            if (UseDB())
                return db.Messages.ToList();
            else
                return Messages.Values.ToList();
        }

        public void update()
        {
            if (UseDB())
                db.SaveChanges();
        }


        public void add(Message obj)
        {
            if (UseDB())
              db.Messages.Add(obj);
            else
                Messages.Add(obj.messageID, obj);
        }

        public void remove(Message obj)
        {
            if (UseDB())
                db.Messages.Remove(obj);
            else
                Messages.Remove(obj.messageID);
        }
    }
}

