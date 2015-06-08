using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace User
{
    public class UserManager : IUserManager
    {

        List<Member> UsersList;
        int newestMemberID;
        List<String> userTypes;
        Context db; //----------------DB

        public UserManager()
        {
            this.UsersList = new List<Member>();
            this.userTypes = new List<string>();
            userTypes.Add("Normal");
            userTypes.Add("Gold");
            userTypes.Add("Silver");
            this.newestMemberID = 10;


            //---------------------Entity------------------------

            db = new Context();

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE Members"); //cleaning table

            db.SaveChanges();
        }


        public int login(string username, string password)
        {
            foreach (Member member in UsersList)
            {
                if ((member.memberUsername.CompareTo(username)==0))
                {
                    if (member.login(password) == true)
                    {
                        db.SaveChanges();
                        return member.memberID;
                    }

                }
                    
            }
            throw new WrongUsernameOrPasswordException();
        }

        public bool logout(int userID)
        {
            Member mem = getMemberByID(userID);
            if(mem.loginStatus==false)    //is user already logged out?
                return false;
            mem.loginStatus = false;        //logging the user out.
            db.SaveChanges();
            return true;
        }

       public int register(string username, string password, string email)
        {
            if (!isNameAvilable(username))
                throw new UsernameIsTakenException();
            UsersList.Add(new Member(newestMemberID, username, password, email));
            newestMemberID++;
            db.SaveChanges();
            return newestMemberID - 1;
        }

        public bool enterForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public int changePassword(int userID, string oldPassword, string newPassword)
        {
            if (getMemberByID(userID).loginStatus == true)
            {
                if (getMemberByID(userID).setPassword(oldPassword, newPassword))
                {
                    db.SaveChanges();
                    return userID;
                }
            }
            throw new UserPasswordIllegalChangeException();       
        }

        public int changeUsername(int userID, string newUsername, string password)
        {
            if (getMemberByID(userID).loginStatus == true)
            {
                if (getMemberByID(userID).memberPassword.CompareTo(password) == 0)
                { // checks if the password is correct
                    if (isNameAvilable(newUsername) == true)
                    {
                        db.SaveChanges();
                        getMemberByID(userID).memberUsername = newUsername;
                        return userID;
                    }
                }
            }
            throw new UsernameIllegalChangeException();
        }

        public int addFriend(int userID, int friendID)
        {
            Member mem = getMemberByID(userID);
            Member friend = getMemberByID(friendID);
            mem.addFriend(friend);
            db.SaveChanges();
            return 1;
        }

        public string getUsername(int userID)
        {
            return getMemberByID(userID).memberUsername;
        }

        public string getPassword(int userID)
        {
            return getMemberByID(userID).memberPassword;
        }

        public void removeFriend(int userID, int friendID)
        {
            Member mem = getMemberByID(userID);
            Member friend = getMemberByID(friendID);
            mem.removeFriend(friend);
            db.SaveChanges();
        }

        public void approveRequest(int notificationID)//delayed for next buid
        {
            throw new NotImplementedException();
        }

        public void deactivate(int userID)
        {
            Member mem = getMemberByID(userID);
            UsersList.Remove(mem);
            db.SaveChanges();
        }

        public bool getConfirmationCodeFromUser(int userID, int code)
        {
            Member mem = getMemberByID(userID);
            return mem.checkConfirmationCodeFromUser(code);
        }

        //Helper methods

        /*
         * returns Member instance that matches the given userID, throws exception if ID not found.
         */
        private Member getMemberByID(int userID)
        {
            foreach (Member member in UsersList)
            {
                if (member.memberID == userID)
                    return member; 
            }
            throw new System.InvalidOperationException("userID not found.");
        }

        private Boolean isNameAvilable(String userName){
            foreach (Member member in UsersList)
            {
                if (member.memberUsername.CompareTo(userName)==0)
                    return false;
            }
            return true;

        }

        public bool IsPasswordValid(string username, int expectancy)
        {
            foreach (Member mem in UsersList)
                if (username.CompareTo(mem.memberUsername) == 0)
                    return mem.PasswordLastChanged.AddDays(expectancy) > DateTime.Now;
            return false;
        }
    }
}
