using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface INotificationManager
    {
        /*
         * Send friend request from sender to reciever
         * return value - true if succeeded, else false
         **/
        bool sendFriendRequest(int sender, int reciever);

        /*
         * Send complaint of complainer about complainOn to copmplainTo
         * return value - true if succeeded, else false
         **/
        void Complain(int complainer, int complainOn, int copmplainTo);

        /*
         * Send notification about new message messageId to array of friends
         * return value - true if succeeded, else false
         */
        void sendPostNotification(int[] friends, int messageId);
    }
}
