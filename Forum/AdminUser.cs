using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    public class AdminUser
    {
        [Key]
        public int id { get; set; }
        public int userID { get; set; }
        public AdminUser(int i)
        {
            userID = i;
        }
    }
}
