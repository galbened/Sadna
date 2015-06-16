﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace User
{
    public class UserManager : IUserManager
    {
        int newestMemberID;
        List<String> userTypes;
        IDBManager<Member> DBman;

        private static UserManager instance = null;

        private UserManager()
        {
            this.userTypes = new List<string>();
            userTypes.Add("Normal");
            userTypes.Add("Gold");
            userTypes.Add("Silver");
            this.newestMemberID = 10;

            DBman = new DBmanager();

            
            //stuff
            /*
            Member newMember1 = new Member(1, "osher", "bl1a", "ossher@ga.com");
            System.Threading.Thread.Sleep(50);
            DBman.add(newMember1);
            DBman.update();
            
            Member newMember2 = new Member(2, "gal", "bla2", "gal@ga.com");
            System.Threading.Thread.Sleep(50);
            Member newMember3 = new Member(3, "tomer", "bl3a", "tom@ga.com");
            System.Threading.Thread.Sleep(50);
            Member newMember4 = new Member(4, "achya", "bla4", "achya@ga.com");
            System.Threading.Thread.Sleep(50);
            Member newMember5 = new Member(5, "lior", "tester", "fiz@ga.com");
            UsersList.Add(newMember1);

            DBman.add(newMember2);
            DBman.add(newMember3);
            DBman.add(newMember4);
            DBman.add(newMember5);

            //this.deactivate(3);
            login("gal", "bla2");
            this.changePassword(2, "bla2", "yokuku");

            DBman.update();
           // db.SaveChanges();

            */
        }

        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserManager();
                return instance;
            }
        }


        public int login(string username, string password)
        {
            List<Member> UsersList = DBman.getAll();
            foreach (Member member in UsersList)
            {
                if ((member.memberUsername.CompareTo(username)==0))
                {
                    if (member.login(password) == true)
                    {
                        saveMembersDB();
                        return member.memberID;
                    }

                }
                    
            }
            throw new WrongUsernameOrPasswordException();
        }

        public bool logout(int userID)
        {
            Member mem = DBman.getObj(userID);
            if(mem.loginStatus==false)    //is user already logged out?
                return false;
            mem.loginStatus = false;        //logging the user out.
            saveMembersDB();
            return true;
        }

       public int register(string username, string password, string email)
        {
            if (!isNameAvilable(username))
                throw new UsernameIsTakenException();
            DBman.add(new Member(newestMemberID, username, password, email));
            newestMemberID++;
            saveMembersDB();
            return newestMemberID - 1;
        }

        public bool enterForum(string forumName)
        {
            throw new NotImplementedException();
        }

        public int changePassword(int userID, string oldPassword, string newPassword)
        {
            Member mem = DBman.getObj(userID);
            if (mem.loginStatus == true)
            {
                if (mem.setPassword(oldPassword, newPassword))
                {
                    saveMembersDB();
                    return userID;
                }
            }
            throw new UserPasswordIllegalChangeException();       
        }

        public int changeUsername(int userID, string newUsername, string password)
        {
            Member mem = DBman.getObj(userID);
            if (mem.loginStatus == true)
            {
                if (mem.memberPassword.CompareTo(password) == 0)
                { // checks if the password is correct
                    if (isNameAvilable(newUsername) == true)
                    {
                        mem.memberUsername = newUsername;
                        saveMembersDB();
                        return userID;
                    }
                }
            }
            throw new UsernameIllegalChangeException();
        }

        public int addFriend(int userID, int friendID)
        {
            //Member mem = getMemberByID(userID);
            //Member friend = getMemberByID(friendID);
            //mem.addFriend(friend);
            //saveMembersDB();
            return 1;
        }

        public string getUsername(int userID)
        {
            Member mem = DBman.getObj(userID);
            return mem.memberUsername;
        }

        public string getPassword(int userID)
        {
            Member mem = DBman.getObj(userID);
            return mem.memberPassword;
        }

        public void removeFriend(int userID, int friendID)
        {
            //Member mem = getMemberByID(userID);
            //Member friend = getMemberByID(friendID);
            //mem.removeFriend(friend);
            saveMembersDB();
        }

        public void approveRequest(int notificationID)//delayed for next build
        {
            throw new NotImplementedException();
        }

        public void deactivate(int userID)
        {
            Member mem = DBman.getObj(userID);
            DBman.remove(mem);
            saveMembersDB();
        }

        public bool getConfirmationCodeFromUser(int userID, int code)
        {
            Member mem = DBman.getObj(userID);
            return mem.checkConfirmationCodeFromUser(code);
        }

        //Helper methods

        /*
         * returns Member instance that matches the given userID, throws exception if ID not found.
         
        private Member getMemberByID(int userID)
        {
            foreach (Member member in UsersList)
            {
                if (member.memberID == userID)
                    return member; 
            }
            throw new System.InvalidOperationException("userID not found.");
        }*/

        private Boolean isNameAvilable(String userName)
        {
            List<Member> UsersList = DBman.getAll();
            foreach (Member member in UsersList)
            {
                if (member.memberUsername.CompareTo(userName)==0)
                    return false;
            }
            return true;

        }

        public bool IsPasswordValid(string username, int expectancy)
        {
            List<Member> UsersList = DBman.getAll();
            foreach (Member mem in UsersList)
                if (username.CompareTo(mem.memberUsername) == 0)
                    return mem.PasswordLastChanged.AddDays(expectancy) > DateTime.Now;
            return false;
        }

        public void saveMembersDB()
        {
            DBman.update();
        }
    }
}
