using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    public class Notification
    {
        int publisherId;

        public Notification(int publisherId)
        {
            this.publisherId = publisherId;
        }

        private void sendNotifications(int[] usersToSend)
        {
            foreach (int userId in usersToSend)
                   sendNotificationtoUser(userId);
        }

        private void sendNotificationtoUser(int userId)
        {

        }
    }
}
