using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    class Moderator
    {
        private int userId;
        private int appointerUserId;
        private DateTime appointmentDate;

        public Moderator(int userId, int appointerUserId)
        {
            this.userId = userId;
            this.appointerUserId = appointerUserId;
            appointmentDate = DateTime.Now;
        }

        public Boolean compareId(int id)
        {
            return userId == id;
        }

        public int GetModeratorId()
        {
            return userId;
        }
    }
}
