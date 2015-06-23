using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumLoggers;

namespace Forum
{
    //Struct includes data relevant for logger
    struct ActionInfo
    {
        public string type;
        public string line;
    }

    public enum SessionReason
    {
        registration,
        loggin
    }

    class Session
    {
        private int userId;
        private string userName;
        private int forumId;
        private string forumName;
        private List<ActionInfo> actions;
        private DateTime startTime;
        private ForumLogger logger;


        public Session(int userId, string userName, int forumId, string forumName, SessionReason sr)
        {
            startTime = DateTime.Now;
            this.userId = userId;
            this.userName = userName;
            this.forumId = forumId;
            this.forumName = forumName;
            actions = new List<ActionInfo>();
            if (sr == SessionReason.loggin)
                AddAction(ForumLogger.TYPE_INFO, "User " + userName + " logged to forum " + forumName);
            else
                if (sr == SessionReason.registration)
                    AddAction(ForumLogger.TYPE_INFO, "User " + userName + " registered to forum " + forumName);
            logger = ForumLogger.GetInstance();
        }

        //Constructor for guests only - can accept only registration requests
        public Session(string userName, int forumId, string forumName)
        {
            this.userName = userName;
            this.forumId = forumId;
            this.forumName = forumName;
            actions = new List<ActionInfo>();
            AddAction(ForumLogger.TYPE_INFO, "Guest is trying to register with userName " + userName + " to forum " + forumName);
            logger = ForumLogger.GetInstance();
        }

        public void AddAction(string type, string line)
        {
            ActionInfo ai = new ActionInfo();
            ai.type = string.Format("{0:M/d/yyyy HH:mm:ss tt}", DateTime.Now) + ". Type: " + type + ". UserName: " + userName + ". UserId: " + userId +": ";
            ai.line = line;
            actions.Add(ai);
        }


        public void EndSession()
        {
            AddAction(ForumLogger.TYPE_INFO, "Session ended for " + userName);
            foreach (ActionInfo ai in actions)
            {
                logger.WriteNoDate(ai.type, ai.line);
            }
        }


        #region Get & Set


        public int UserId { get; private set; }

        public string UserName { get; private set; }

        public int ForumId { get; private set; }

        public List<ActionInfo> Actions { get; private set; }

        #endregion

    }
}
