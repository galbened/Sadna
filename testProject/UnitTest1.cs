using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testProject
{
    [TestClass]
    public class userTest
    {
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p","osher"};
        String[] passwords = {"123456","abcdef"};
        Interfaces.userManager um = User.userManager.getInstance();

        /*
         * Testing regestration:
         * regestration of two different usernames returns two different IDs
         * second regestration with a username returns -1
         */
        [TestMethod]
        public void regestrationTest()
        {
            int id1 = um.register(userNames[0], passwords[0]);
            int id2 = um.register(userNames[1], passwords[1]);
            int id3 = um.register(userNames[0], passwords[0]);
            Assert.AreNotEqual(id1, id2);
            Assert.AreEqual(id3, -1);
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
        public void loginAndLogoutTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);
            Assert.AreEqual(um.login(userNames[4], passwords[4]), -1);
            Assert.IsTrue(um.logout(id1));
            Assert.IsFalse(um.logout(id1));
        }

        /*
         * Testing change username:
         * checking that the username changes
         */
        [TestMethod]
        public void changeUsernameTest()
        {
            int id1 = um.changeUsername(userNames[0], userNames[1], passwords[0]);
            Assert.AreEqual(id1, -1); //should be logedin
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changeUsername(userNames[0], userNames[1], passwords[0]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(userNames[1].CompareTo(um.getUsername(id1)),0);
            id1 = um.changeUsername(userNames[1], userNames[0], passwords[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getUsername(id1)), 0);
            Assert.AreEqual(um.changeUsername(userNames[1], userNames[0], passwords[0]), -1);//user details incorrect
            Assert.IsTrue(um.logout(id1));
        }

        /*
         * Testing change password:
         * checking that the password changes
         */
        [TestMethod]
        public void changePasswordTest()
        {
            int id1 = um.changePassword(userNames[0], passwords[0], passwords[1]);
            Assert.AreEqual(id1, -1); //should be logedin
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changePassword(userNames[0], passwords[0], passwords[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(passwords[1].CompareTo(um.getPasswords(id1)), 0);
            id1 = um.changePassword(userNames[0], passwords[1], passwords[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getPasswords(id1)), 0);
            Assert.AreEqual(um.changeUsername(userNames[1], userNames[0], passwords[0]), -1);//user details incorrect
            Assert.IsTrue(um.logout(id1));
        }

        /*
         * Testing add friend:
         * checking that friend was added after the friend approved
         */
        [TestMethod]
        public void changePasswordTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.login(userNames[1], passwords[1]);
            int notID = um.addFriend(id1, id2);
            Assert.AreNotEqual(notID, -1); //notification should be sent
            Assert.AreEqual(um.addFriend(id1,id2),-1);//notification shouldn't be sent
            Assert.IsTrue(um.logout(id1));
            Assert.IsTrue(um.logout(id2));
        }

        /*
         * Testing deactivate account:
         * checking that the user account was diactivated
         */
        [TestMethod]
        public void changePasswordTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            um.diactivate(id1);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);//should fail - users not exists
        }
    }
}
