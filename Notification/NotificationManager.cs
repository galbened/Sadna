using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Notification
{
    class NotificationManager : INotificationManager
    {
        public bool sendFriendRequest(int sender, int reciever)
        {
            throw new NotImplementedException();
        }

        public void Complain(int complainer, int complainOn, int copmplainTo)
        {
            throw new NotImplementedException();
        }

        public void sendPostNotification(int[] friends, int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
