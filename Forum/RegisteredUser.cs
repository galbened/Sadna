using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    public class RegisteredUser
    {
        [Key]
        public int id { get; set; }
        public int userID { get; set; }
        public RegisteredUser(int i)
        {
            userID = i;
        } 
    }
}
