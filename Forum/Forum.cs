using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Policy.Policy;

namespace Forum
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

        public void createSubForum(string topic)
        {
            subForums.Add(new SubForum(topic));
        }

        public Policy getPolicy()
        {
            return poli;
        }

        public void addAdmin(int userId)
        {
            adminsID.Add(userId);
        }

        public void removeAdmin(int userId)
        {
            adminsID.Remove(userId);
        }

        public void showSubForums()
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

        public void register(string username, string password)
        {
            int id = User.getUserId(username, password);
            if (!(registeredUsersID.Contains(id)))
                registeredUsersID.Add(id);
        }

        public void login(string username, string password)
        {
            int id = User.getUserId(username, password);
            if (registeredUsersID.Contains(id))
                if (!(logedUsersId.Contains(id)))
                    logedUsersId.Add(id);
        }

        public void logout(int userId)
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
