using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using User;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace User.UnitTests
{
 
    [TestClass]
    public class UserUnitTest
    {
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "123456", "abcdef" };
        IUserManager um = UserManager.Instance;

        /*
 * Testing regestration:
 * regestration of two different usernames returns two different IDs
 * second regestration with a username returns -1
 */
        [TestMethod]
        public void registrationReturnsDiffIDTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            int id2 = um.register(userNames[1], passwords[1], emails[1]);
            Assert.AreNotEqual(id1, id2);
            um.deactivate(id1);
            um.deactivate(id2);
        }

        [TestMethod]
        [ExpectedException(typeof(UsernameIsTakenException), "Username entered is taken.")]
        public void registrationAgainFailTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            int id3 = um.register(userNames[0], passwords[0], emails[0]);
            //Assert.AreEqual(id3, -1);
            um.deactivate(id1);
        }

        /*
         * Testing login and logout:
         * first login should succeed and return the UserId
         * second login should fail and return 0
         * third login without registration first should fail and return 0
         * first logout should succeed
         * second logout should fail
         */
        [TestMethod]
        public void registerLoginLogoutTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), id1);
            Assert.IsTrue(um.logout(id1));
            um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
        }

        [TestMethod]
        public void loginTwiceFailTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            //Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);
            um.deactivate(id1);
        }

        [TestMethod]
        public void loginWithoutRegisterTest()
        {
            //Assert.AreEqual(um.login(userNames[4], passwords[1]), -1);
        }

        [TestMethod]
        public void logoutTwiceFailsTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            Assert.IsTrue(um.logout(id1));
            Assert.IsFalse(um.logout(id1));
            id1 = um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
        }


        /*
         * Testing change username:
         * checking that the username changes
         */
        [TestMethod]
        [ExpectedException(typeof(UsernameIllegalChangeException), "User is not logged in - illegal change.")]
        public void changeUsernameNotLogedinTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.changeUsername(id1, userNames[1], passwords[0]);
            //Assert.AreEqual(id1, -1); //should be logedin
            id1 = um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
        }

        [TestMethod]
        public void changeUsernameTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changeUsername(id1, userNames[1], passwords[0]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(userNames[1].CompareTo(um.getUsername(id1)), 0);
            id1 = um.changeUsername(id1, userNames[0], passwords[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getUsername(id1)), 0);
            um.deactivate(id1);
        }

        [TestMethod]
        [ExpectedException(typeof(UsernameIllegalChangeException), "Entered details are wrong - illegal change.")]
        public void changeUsernameIncorrectDetailsTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            //Assert.AreEqual(um.changeUsername(id1, userNames[1], passwords[1]), -1);//user details incorrect
            um.changeUsername(id1, userNames[1], passwords[1]);//user details incorrect
            um.deactivate(id1);
        }

        /*
         * Testing change password:
         * checking that the password changes
         */
        [TestMethod]
        [ExpectedException(typeof(UserPasswordIllegalChangeException), "User is not logged in - illegal password change.")]
        public void changePasswordNotLogedinTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.changePassword(id1, passwords[0], passwords[1]);
            //Assert.AreEqual(id1, -1); //should be logedin
            id1 = um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
        }
        /*
        [TestMethod]
        public void changePasswordTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changePassword(id1, passwords[0], passwords[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(passwords[1].CompareTo(um.getPassword(id1)), 0);
            id1 = um.changePassword(id1, passwords[1], passwords[0]);
            Assert.AreEqual(passwords[0].CompareTo(um.getPassword(id1)), 0);
            um.deactivate(id1);
        }*/

        [TestMethod]
        [ExpectedException(typeof(UserPasswordIllegalChangeException), "Entered details are wrong - illegal password change.")]
        public void changePasswordIncorrectDetailsTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.changePassword(id1, passwords[1], passwords[0]), -1);//user details incorrect
            Assert.IsTrue(um.logout(id1));
            um.deactivate(id1);
        }

        /*
         * Testing add friend:
         * checking that friend was added after the friend approved
         */
        [TestMethod]
        public void addFriendTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            int id2 = um.register(userNames[1], passwords[1], emails[1]);
            id1 = um.login(userNames[0], passwords[0]);
            int notID = um.addFriend(id1, id2);
            Assert.AreNotEqual(notID, -1); //notification should be sent
            um.deactivate(id1);
            um.deactivate(id2);
        }

        /*
         * Testing deactivate account:
         * checking that the user account was diactivated
         */
        [TestMethod]
        [ExpectedException(typeof(WrongUsernameOrPasswordException), "Username does not exist.")]
        public void deactivateTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
            //Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);//should fail - users not exists
            um.login(userNames[0], passwords[0]);//should fail - users not exists
        }
    }
}
