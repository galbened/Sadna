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
        //private String memberPassword;
        String memberPassword;
        String memberEmail;
        Boolean loggerStatus = false;
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
            this.accountStatus = true; //user that not yet confirmed is email should be false - TODO when sending to mail is done
            this.FriendsList = new List<Member>();

            //creating confirmation code and sending it to user email
            this.confirmationCode = creatingConfirmationCodeAndSending(memberEmail);
        }

        public void addFriend(Member friend)
        {
            FriendsList.Add(friend);
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

        public int getMemberID()
        {
            return memberID;
        }

        public String getMemberUsername()
        {
            return memberUsername;
        }

        
        /*
         * gets a pasword and compares it with the uesr's password,
         * returns true if it match
         * */
        public Boolean login(String password)
        {
            if ((password.CompareTo(memberPassword) == 1) && (loggerStatus == false))
            {
                loggerStatus = true;
                return true;
            }
            return false;
        }



        public Boolean getLoggerStatus()
        {
            return loggerStatus;
        }

        public void setLoggerStatus(Boolean wantedStatus) //sets status false if logging out, true if logging in
        {
            loggerStatus = wantedStatus;
        }


        /*
         * it does not check if the new password is legit
         * */
        public Boolean setPassword(string oldPassword, string newPassword)
        {
            if (oldPassword.Equals(memberPassword))
            {
                memberPassword = newPassword;
                return true;
            }
            return false;
        }

        public void setUsername(string  newusername)
        {
            memberUsername = newusername;
        }


    }
}
