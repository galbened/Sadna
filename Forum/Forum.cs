using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.User;

namespace ForumsSystem
{
    public class Forum
    {
        private List<int> registeredUsersID,logedUsersId, adminsID;
        private string forumName;
        private int forumID;
        private List<SubForum> subForums;
        private Policy poli;

        public Forum(int id, string name)
        {
            forumName = name;
            forumID = id;
            registeredUsersID = new List<int>();
            adminsID = new List<int>();
            logedUsersId = new List<int>();
            subForums = new List<SubForum>();
            poli = new Policy();
        }

        public void CreateSubForum(string topic)
        {
            subForums.Add(new SubForum(topic));
        }

        public Policy getPolicy()
        {
            return poli;
        }

        public void AddAdmin(int userId)
        {
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

        public Boolean isAdmin(int userId)
        {
            return adminsID.Contains(userId);
        }

        public void Register(string username, string password)
        {
            int id = User.getUserId(username, password);
            if (!(registeredUsersID.Contains(id)))
                registeredUsersID.Add(id);
        }

        public void Login(string username, string password)
        {
            int id = User.getUserId(username, password);
            if (registeredUsersID.Contains(id))
                if (!(logedUsersId.Contains(id)))
                    logedUsersId.Add(id);
        }

        public void Logout(int userId)
        {
            logedUsersId.Remove(userId);
        }

        public void setPolicy(int numOfModerators, string structureOfPassword, string degreeOfEnsuring)
        {
            poli.setNumOfModerators(numOfModerators);
            poli.setstructureOfPassword(structureOfPassword);
            poli.setdefreeOfEnsuring(degreeOfEnsuring);
        }


  
    }
}
