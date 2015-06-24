using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    public class Moderator
    {
        [Key]
        public int id { get; set; }
        public int userId { get; set; }
        //private int userId;
        public int appointerUserId { get; set; }
        //private int appointerUserId;
        public DateTime appointmentDate { get; set; }
        //private DateTime appointmentDate;

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
