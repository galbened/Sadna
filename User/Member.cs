using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Notification;

namespace User
{
    public class Member
    {
        public int memberID { get; set; }
        public String memberUsername { get; set; }
        public String memberPassword { get; set; }
        public String identifyQuestion { get; set; }
        public String identifyAnswer { get; set; }
        public DateTime passwordLastChanged { get; set; }
        public String memberEmail { get; set; }
        public Boolean loginStatus { get; set; }
        public memberState currentState { get; set; }
        public Boolean accountStatus { get; set; }
        public int confirmationCode { get; set; }
        //public virtual List<Member> FriendsList { get; set; }
        public List<Member> FriendsList { get; set; }
        
        public virtual List<Password> pastPasswords { get; set; }

        private const string error_passwordAlreadyUsed = "Password already used in past"; 
        private const string error_oldPasswordDoesNotMatch = "Old password doesn't match the active one"; 

        public Member()
        {

        }
        public Member(int memberID, String memberUsername, String memberPassword, String memberEmail)
        {
            //this.memberID = memberID;
            this.memberUsername = memberUsername;
            this.memberPassword = "firstPass";
            passwordLastChanged = DateTime.Now;
            this.memberEmail = memberEmail;
            this.loginStatus = false;
            this.accountStatus = true; //user that not yet confirmed his email should be false - TODO when sending to mail is done
            //this.FriendsList = new List<Friendship>();
            currentState = new stateNormal();       // new user begins as a Normal user
            pastPasswords = new List<Password>();
            setPassword("firstPass", memberPassword);
            FriendsList = new List<Member>();
            //creating confirmation code and sending it to user email
            this.confirmationCode = creatingConfirmationCodeAndSending(memberUsername, memberEmail);
        }

        public DateTime PasswordLastChanged
        {
            get { return passwordLastChanged; }
        }

        public void addFriend(Member friend)
        {
            FriendsList.Add(friend);
           //FriendsList.Add(new Friendship(friend));
        }

        public void removeFriend(Member friend)
        {
            FriendsList.Remove(friend);
            /*
            Friendship del = null;
            foreach(Friendship f in FriendsList)
            {
                if (f.friend == newFriend)
                    del = f;
            }
            FriendsList.Remove(del);
             */
        }

        private int creatingConfirmationCodeAndSending(String userName, String memberEmail)
        {
            Random rnd = new Random();
            int newConfirmationCode=rnd.Next(1000000,9999999);
            Notification.Notification.SendConfirmationMail(userName, memberEmail, newConfirmationCode);
            return newConfirmationCode;
        }

        public Boolean checkConfirmationCodeFromUser(int code)
        {
            if(this.confirmationCode == code)
            {
                this.accountStatus = true; //activating the account
                return true;
            }
            return false;
        }
        
        /*
         * gets a pasword and compares it with the uesr's password,
         * returns true if it match
         * */
        public Boolean login(String password)
        {
            if ((password.CompareTo(memberPassword) == 0) && (loginStatus == false))
            {
                loginStatus = true;
                return true;
            }
            return false;
        }

        /*
         * it does not check if the new password is legit
         * */
        public Boolean setPassword(string oldPassword, string newPassword)
        {
            //first time changing password - incoming from instructor (no need to check anything)
            if(memberPassword == "firstPass")
            {
                this.memberPassword = newPassword;
                pastPasswords.Add(new Password(this.memberPassword));
                return true;
            }

            //Checking if old password matches the active one
            if(memberPassword != oldPassword)
                throw new ArgumentException(error_oldPasswordDoesNotMatch);

            //Checking old passwords
            foreach(Password pass in pastPasswords) 
            {
                if (pass.passString == newPassword)
                    throw new ArgumentException(error_passwordAlreadyUsed);
            }

            //All in order, changing password and adding the last one to past passwords
            this.memberPassword = newPassword;
            pastPasswords.Add(new Password(this.memberPassword));

            return true;
        }
        
        /*
         * this method changes the state of the current member to the next state
         * */
        public void nextState()
        {
            if (currentState.getType().CompareTo("Gold") == 0)
                return;
            else if (currentState.getType().CompareTo("Silver") == 0)
                currentState = new stateGold();
            else if (currentState.getType().CompareTo("Normal") == 0)
                currentState = new stateSilver();
        }

        /*
         * this method changes the state of the current member to the previous state
         * */
            public void prevState()
            {
                if (currentState.getType().CompareTo("Gold") == 0)
                    currentState = new stateSilver();
                else if (currentState.getType().CompareTo("Silver") == 0)
                    currentState = new stateNormal();
                else if (currentState.getType().CompareTo("Normal") == 0)
                    return;
            }

    }
}
