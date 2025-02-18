﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User;

namespace Forum
{
    public class Forum
    {
        private List<int> registeredUsersID,logedUsersId, adminsID;
        private string forumName;
        private int forumID;
        private List<SubForum> subForums;
        private Policy poli;
        private static int subForumIdCounter;
        private UserManager usrMngr;
        private const string error_existTitle = "Cannot create subForum with already exit title";
        private const string error_notAdmim = "Cannot create subForum with not admin caller ID";
        private const string error_forumID = "No such subForum: ";

        public Forum(string name, int id)
        {
            forumName = name;
            forumID = id;
            registeredUsersID = new List<int>();
            adminsID = new List<int>();
            logedUsersId = new List<int>();
            subForums = new List<SubForum>();
            poli = new Policy();
            usrMngr = new UserManager();
            subForumIdCounter = 100;
        }

        public int CreateSubForum(string topic, int callerUserId)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.Topic.CompareTo(topic) == 0)
                    throw new ArgumentException(error_existTitle);
            if (adminsID.Contains(callerUserId))
            {
                subForums.Add(new SubForum(topic, subForumIdCounter));
                subForumIdCounter++;
                return subForumIdCounter - 1;
            }
            else
                throw new ArgumentException(error_notAdmim);
        }

        public Policy Poli
        {
            get { return poli; }
        }

        public void AddAdmin(int userId)
        {
            if (registeredUsersID.Contains(userId))
                adminsID.Add(userId);
        }

        public void RemoveAdmin(int userId)
        {
            adminsID.Remove(userId);
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
            if (!(poli.IsValid(password)))
                return -1;

            int id = usrMngr.register(username, password, mail);
            if (id < 0)
                return -2;
            if (!(registeredUsersID.Contains(id)))
                registeredUsersID.Add(id);
            return id;
        }

        public int Login(string username, string password)
        {
            int id = usrMngr.login(username, password);
            if (registeredUsersID.Contains(id))
                if (!(logedUsersId.Contains(id)))
                    logedUsersId.Add(id);
                else
                    return -1;
            return id;
        }

        public Boolean Logout(int userId)
        {
            if (!(logedUsersId.Contains(userId)))
                return false;
            usrMngr.logout(userId);
            logedUsersId.Remove(userId);
            return true;
        }

        public void SetPolicy(int numOfModerators, string degreeOfEnsuring,
                       Boolean uppercase, Boolean lowercase, Boolean numbers,
                       Boolean symbols, int minLength)
        {
            poli.ModeratorNum=numOfModerators;
            poli.PasswordEnsuringDegree=degreeOfEnsuring;
            poli.UpperCase=uppercase;
            poli.LowerCase=lowercase;
            poli.Numbers=numbers;
            poli.Symbols=symbols;
            poli.MinLength=minLength;
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

        internal void AddModerator(int userId, int subForumId)
        {
            if (registeredUsersID.Contains(userId))
                foreach (SubForum sbfrm in subForums)
                    if (sbfrm.SubForumId == subForumId)
                        sbfrm.AddModerator(userId);
        }

        internal void RemoveModerator(int userId, int subForumId)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.SubForumId == subForumId)
                    sbfrm.RemoveModerator(userId);
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
            registeredUsersID.Remove(userId);
        }

        internal int GetSubForumId(string topic)
        {
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.Topic.CompareTo(topic) == 0)
                    return sbfrm.SubForumId;
            throw new ArgumentException(error_forumID + topic); ;
        }

        internal Boolean RemoveSubForum(int subForumId, int callerUserId)
        {
            SubForum tmp = null;
            foreach (SubForum sbfrm in subForums)
                if (sbfrm.SubForumId == subForumId)
                    tmp = sbfrm;
            if (adminsID.Contains(callerUserId))
            {
                subForums.Remove(tmp);
                return true;
            }
            return false;
        }

        internal bool IsValidPassword(string password)
        {
            return poli.IsValid(password);
        }
    }
}
