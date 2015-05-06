using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    class Member
    {
        int memberID;
        String memberUsername;
        String memberPassword;

        String memberEmail;
        Boolean loggerStatus;
        memberState currentState;
        Boolean accountStatus;
        int confirmationCode;
        List<Member> FriendsList;

        public Member(int memberID, String memberUsername, String memberPassword, String memberEmail)
        {
            this.memberID = memberID;
            this.memberUsername = memberUsername;
            this.memberPassword = memberPassword;
            this.memberEmail = memberEmail;
            this.loggerStatus = false;
            this.accountStatus = true; //user that not yet confirmed his email should be false - TODO when sending to mail is done
            this.FriendsList = new List<Member>();
            currentState = new stateNormal();       // new user begins as a Normal user

            //creating confirmation code and sending it to user email
            this.confirmationCode = creatingConfirmationCodeAndSending(memberEmail);
        }

        public int MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        public String MemberUsername
        {
            get { return memberUsername; }
            set { memberUsername = value; }
        }
        public Boolean LoggerStatus
        {
            get { return loggerStatus; }
            set { loggerStatus = value; }
        }
        public String MemberPassword
        {
            get { return memberPassword; }
            //set { memberPassword = value; }
        }


        public void addFriend(Member friend)
        {
            FriendsList.Add(friend);
        }

        public void removeFriend(Member friend)
        {
            FriendsList.Remove(friend);
        }

        private int creatingConfirmationCodeAndSending(String memberEmail)
        {
            Random rnd = new Random();
            int newConfirmationCode=rnd.Next(1000000,9999999);
            sendingConfirmationCodeToMail(newConfirmationCode);
            return newConfirmationCode;
        }

        void sendingConfirmationCodeToMail(int code)
        {
            //TODO
            return;
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
            if ((password.CompareTo(memberPassword) == 0) && (loggerStatus == false))
            {
                loggerStatus = true;
                return true;
            }
            return false;
        }

        /*
         * it does not check if the new password is legit
         * */
        public Boolean setPassword(string oldPassword, string newPassword)
        {
            if (oldPassword == memberPassword)
            {
                memberPassword = newPassword;
                return true;
            }
            return false;
        }
    }
}
