using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Interfaces;

namespace Notification
{
    public static class Notification
    {
        private static string systemMail = "drpcbgu@gmail.com";

        public static void SendConfirmationMail(string userName, string userMail, int code)
        {
            string title = "Confirmation Code to Kashkasha Forums";
            string body = "Hello Mr\\Mrs " + userName + " and welcome to Kashkasha Forums!\n "
                                 + "Your confirmation code is " + code.ToString();
            SendMailToUser(userMail, title, body);   
        }

        public static void SendFriendRequestNotification(string userNameFrom, string userNameTo, string emailTo)
        {
            string title = "Friend Request Accepted";
            string body = "Hello " + userNameTo + "!\n" + userNameFrom + " wants to be your friend in Kashkasha Forums!";
            SendMailToUser(emailTo, title, body);
        }

        public static void SendNotificationToGroup(string userNameFrom, List<string> userNamesTo, List<string> mailsTo)
        {
            for (int i=0; i < userNamesTo.Count; i++)
            {
                string title = "New notification accepted!";
                string body = "Hello " + userNamesTo.ElementAt(i) + "!\n"
                            + userNameFrom + " performed new action in Kashkasha Forums!";
                SendMailToUser(mailsTo.ElementAt(i), title, body);
            }
        }

        public static void SendComplaintNotification(string complainer, string complainToUser, string complainOnUser, string complainToMail)
        {
            string title = "Complaint about user in your forum accepted";
            string body = "Hello " + complainToUser + "!\n" 
                        + complainer + " complains on " + complainOnUser;
            SendMailToUser(complainToMail, title, body);
        }








        /*
        private int _publisherId;

        public Notification(int publisherId)
        {
            this.publisherId = publisherId;
        }

        private void sendNotifications(int[] usersToSend)
        {

            foreach (int userId in usersToSend)
                   sendNotificationtoUser(userId);
        }
        */
        private static void SendMailToUser(string targetMail, string title, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(systemMail);
            mail.Subject = title;
            mail.To.Add(targetMail);
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("drpcbgu", "123drpc123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

    }
}
