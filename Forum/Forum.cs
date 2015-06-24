using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;
using Interfaces;
using ForumLoggers;

namespace Forum
{
    public class Forum
    {
        public int forumID { get; set; }
        private List<int> logedUsersId, adminsID;
        public List<RegisteredUser> registeredUsers { get; set; }
        //private int forumID;
        public static Policy poli { get; set; }
        //private Policy poli;
        public static int subForumIdCounter { get; set; }
        //private static int subForumIdCounter;
        private IUserManager usrMngr;

        private const string error_existTitle = "Cannot create subForum with already exit title";
        private const string error_notAdmim = "Cannot create subForum with not admin caller ID";
        private const string error_forumID = "No such Forum: ";
        private const string error_expiredPassword = "Password expired need to change";
        private const string error_subForumID = "No such SubForum: ";
        private const string error_invalidPassword = "Password is not valid according to policy";
        private const string error_failedRegistrarion = "Failed to register new user in user manager";
        private const string error_accessDenied = "User has no permissions to perform this operation";


        public string forumName { get; set; }
        public List<SubForum> subForums { get; set; }
        //public List<SubForum> subForums { get; private set; }

        private static Dictionary<int, Session> sessions { get; set; }
        //private Dictionary<int, Session> sessions;

        public Forum() 
        {
        }

        public Forum(string name, int id)
        {
            forumName = name;
            forumID = id;
            registeredUsers = new List<RegisteredUser>();
            registeredUsers.Add(new RegisteredUser(1));
            adminsID = new List<int>();
            adminsID.Add(1);
            logedUsersId = new List<int>();
            logedUsersId.Add(1);
            subForums = new List<SubForum>();
            poli = new Policy();
            usrMngr = UserManager.Instance;
            subForumIdCounter = 100;
            sessions = new Dictionary<int, Session>();
            sessions.Add(1, new Session("Admin", forumID, forumName));
        }

        public List<int> GetForumAdmins()
        {
            return adminsID;
        }

        public int CreateSubForum(int userRequesterId, string topic)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to create subForum " + topic + " in forum " + forumID);
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId)))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied); 
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                foreach (SubForum sbfrm in subForums)
                    if (sbfrm.Topic.CompareTo(topic) == 0)
                        throw new ArgumentException(error_existTitle);
                subForums.Add(new SubForum(topic, subForumIdCounter));
                subForumIdCounter++;
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "SubForum " + topic + "with Id " +(subForumIdCounter - 1)+" created successfully in forumId " + forumID);   
            return subForumIdCounter - 1;
        }

        public Policy Poli
        {
            get { return poli; }
        }

        public void AddAdmin(int userRequesterId, int userId)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to add admin " + userId + " to forum " + forumID);
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId)))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied); 
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                if (registeredUsers.Any(ru => ru.userID == userId))
                    adminsID.Add(userId);
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "Admin " + userId + " was added to forumId " + forumID);
        }

        public void RemoveAdmin(int userRequesterId, int userId)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to remove admin " + userId + " to forum " + forumID);
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId)))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied);
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                adminsID.Remove(userId);
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "Admin " + userId + " was removed from forumId " + forumID);
        }

        public void ShowSubForums()
        {
            foreach (SubForum sf in subForums)
            {
                Console.Write(sf.ToString());
            }
        }

        public Boolean IsAdmin(int userId)
        {
            return adminsID.Contains(userId);
        }
        
        public int Register(string username, string password, string mail)
        {
            Session guestSession = new Session(username, forumID, forumName);
            if (!(poli.IsValid(password)))
            {
                guestSession.AddAction(ForumLogger.TYPE_ERROR, username + " registration failed " + error_invalidPassword);
                guestSession.EndSession();
                throw new ArgumentException(error_invalidPassword);
            }
            int userId = -1;
            try
            {
                userId = usrMngr.register(username, password, mail);
            }
            catch(Exception ex)
            {
                guestSession.AddAction(ForumLogger.TYPE_ERROR, username + " registration failed " + ex.Message);
                guestSession.EndSession();
                throw new ArgumentException(error_failedRegistrarion + ex.Message);
            }
            if (userId > -1)
            {
                if (!(registeredUsers.Any(ru => ru.userID == userId)))
                    registeredUsers.Add(new RegisteredUser(userId));

                //if (!(registeredUsersID.Contains(userId)))
                //    registeredUsersID.Add(userId);
                if (!(logedUsersId.Contains(userId)))
                    logedUsersId.Add(userId);
            }
            guestSession.EndSession();
            Session se = new Session(userId, username, forumID, forumName, SessionReason.registration);
            sessions.Add(userId, se);
            return userId;
        }

        public int Login(string username, string password)
        {
            Boolean canLogin = usrMngr.IsPasswordValid(username, poli.PasswordExpectancy);
            if (!canLogin)
                throw new ArgumentException(error_expiredPassword);
            int userId = usrMngr.login(username, password);

            //if (registeredUsersID.Contains(userId))
            if (registeredUsers.Any(ru => ru.userID == userId))
                if (!(logedUsersId.Contains(userId)))
                    logedUsersId.Add(userId);
                else
                    return -1;
            Session se = new Session(userId, username, forumID, forumName, SessionReason.loggin);
            sessions.Add(userId, se);
            return userId;
        }

        public Boolean Logout(int userId)
        {
            if (!(logedUsersId.Contains(userId)))
                return false;
            usrMngr.logout(userId);
            logedUsersId.Remove(userId);
            Session se = GetSession(userId);
            se.EndSession();
            sessions.Remove(userId);
            return true;
        }

        public void SetPolicy(int userRequesterId, int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to set policy in forum " + forumID);
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId)))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied);
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                poli.ModeratorNum = numOfModerators;
                poli.PasswordEnsuringDegree = degreeOfEnsuring;
                poli.UpperCase = uppercase;
                poli.LowerCase = lowercase;
                poli.Numbers = numbers;
                poli.Symbols = symbols;
                poli.MinLength = minLength;
                poli.PasswordExpectancy = 20;
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "Policy was changed successfully in forum " + forumID);
        }

        public int ForumID
        {
            get { return forumID; }
        }

        internal Boolean IsModerator(int userId, int subForumId)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.SubForumId == subForumId)
                    return sbfrm.IsModerator(userId);
            throw new ArgumentException(error_forumID+subForumId);
        }

        internal void AddModerator(int userRequesterId, int subForumId, int moderatorId)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to add moderator " + moderatorId + " to forum " + forumID + ", subForum: " + subForumId);

            SubForum sf = GetSubForum(subForumId);
            List<int> moderators = sf.GetModeratorsIds();
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId) && (!CheckExisting(moderators, subForumId))))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied);
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                if (registeredUsers.Any(ru => ru.userID == moderatorId))
                {
                    Boolean flag = false;
                    //if (registeredUsersID.Contains(moderatorId))
                    foreach (SubForum sbfrm in subForums)
                        if ((sbfrm.SubForumId == subForumId) && (sbfrm.NumOfModerators() < poli.ModeratorNum))
                        {
                            sbfrm.AddModerator(moderatorId, userRequesterId);
                            flag = true;
                        }
                    if (!flag)
                        throw new ArgumentException("too much moderators");
                }
                
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "Moderator " + moderatorId + " added to forum " + forumID + ", subForum: " + subForumId);
        }

        internal void RemoveModerator(int userRequesterId, int userId, int subForumId)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to remove moderator " + userId + " from forum " + forumID + ", subForum: " + subForumId);

            SubForum sf = GetSubForum(subForumId);
            List<int> moderators = sf.GetModeratorsIds();
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId) && (!CheckExisting(moderators, subForumId))))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied);
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                foreach (SubForum sbfrm in subForums)
                    if (sbfrm.SubForumId == subForumId)
                        sbfrm.RemoveModerator(userId);
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "Moderator " + userId + " removed from forum " + forumID + ", subForum: " + subForumId);
        }

        internal void SetSubTopic(string topic, int subForumId)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.SubForumId == subForumId)
                    sbfrm.Topic = topic;
        }

        internal int CompareName(string name)
        {
            return this.forumName.CompareTo(name);
        }

        internal void UnRegister(int userId)
        {
            logedUsersId.Remove(userId);
            registeredUsers.RemoveAll(ru => ru.userID == userId);
            //registeredUsersID.Remove(userId);
            usrMngr.deactivate(userId);
        }

        internal int GetSubForumId(string topic)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.Topic.CompareTo(topic) == 0)
                    return sbfrm.SubForumId;
            throw new ArgumentException(error_forumID + topic); ;
        }

        internal SubForum GetSubForum(int subForumId)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.SubForumId == subForumId)
                    return sbfrm;
            throw new ArgumentException(error_forumID + subForumId); ;
        }

        internal Boolean RemoveSubForum(int userRequesterId, int subForumId)
        {
            Session se = GetSession(userRequesterId);
            se.AddAction(ForumLogger.TYPE_INFO, "Trying to remove subForum " + subForumId + " in forum " + forumID);
            if ((userRequesterId != 1) && (!CheckExisting(adminsID, userRequesterId)))
            {
                se.AddAction(ForumLogger.TYPE_ERROR, error_accessDenied); 
                throw new UnauthorizedAccessException(error_accessDenied);
            }
            try
            {
                SubForum tmp = null;
                foreach (SubForum sbfrm in subForums)
                    if (sbfrm.SubForumId == subForumId)
                        tmp = sbfrm;
                if (adminsID.Contains(userRequesterId))
                {
                    subForums.Remove(tmp);
                    return true;
                }
            }
            catch (Exception ex)
            {
                se.AddAction(ForumLogger.TYPE_ERROR, ex.Message);
                throw ex;
            }
            se.AddAction(ForumLogger.TYPE_INFO, "SubForum " + subForumId + "removed from forum " + forumID);
            return false;
        }

        internal bool IsValidPassword(string password)
        {
            return poli.IsValid(password);
        }

        internal Boolean isUserRegistered(int userId)
        {
            return (registeredUsers.Any(ru => ru.userID == userId));
            //return registeredUsersID.Contains(userId);
        }

        internal string GetSubForumTopic(int forumId,int subForumId)
        {
            for (int i=0 ; i < subForums.Count; i++)
            {
                if (subForums.ElementAt(i).SubForumId == subForumId)
                    return subForums.ElementAt(i).Topic;
            }
            throw new ArgumentException(error_subForumID + " SubForumId= " + subForumId);
        }

        internal string GetUserName(int userId)
        {
            return usrMngr.getUsername(userId);
        }

        public List<string> GetSessionHistory(int userIdSession)
        {
            Session se = sessions[userIdSession];
            List<string> ans = new List<string>();
            foreach (ActionInfo ai in se.Actions)
            {
                ans.Add(ai.line);
            }
            return ans;
        }

        public string GetUserMail(int userId)
        {
            string ans = usrMngr.GetUserMail(userId);
            return ans;
        }

        internal List<int> GetMembersNoAdminIds()
        {
            List<int> ans = new List<int>();
            foreach (RegisteredUser ru in registeredUsers)
            {
                if (!IsAdmin(ru.userID))
                    ans.Add(ru.userID);
            }
            return ans;          
        }


        internal List<int> GetMembersNoModeratorIds(int subForumId)
        {
            List<int> ans = new List<int>();
            foreach (RegisteredUser ru in registeredUsers)
            {
                if (!IsModerator(ru.userID, subForumId))
                    ans.Add(ru.userID);
                //if (!IsModerator(id, subForumId))
                //    ans.Add(id);
            }
            return ans;
        }





        private bool CheckExisting(List<int> list, int find)
        {
            foreach (int obj in list)
            {
                if (obj == find)
                    return true;
            }
            return false;
        }


        private Session GetSession(int userId)
        {
            try
            {
                Session ans = sessions[userId];
                if (ans != null)
                    return ans;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User " + userId + " has no open session!: " + ex.Message);
            }
            return null;
        }
    }
}
