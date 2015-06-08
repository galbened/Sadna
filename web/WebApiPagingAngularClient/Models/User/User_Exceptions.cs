using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class WrongUsernameOrPasswordException : Exception
    {
        public WrongUsernameOrPasswordException() : base() { }
    }
    public class UsernameIsTakenException : Exception
    {
        public UsernameIsTakenException() : base() { }
    }
    public class UserPasswordIllegalChangeException : Exception
    {
        public UserPasswordIllegalChangeException() : base() { }
    }
    public class UsernameIllegalChangeException : Exception
    {
        public UsernameIllegalChangeException() : base() { }
    }
}
