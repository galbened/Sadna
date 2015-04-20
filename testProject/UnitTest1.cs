using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using User;
using Forum;

namespace testProject
{
    [TestClass]
    public class userTest
    {
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p","osher"};
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = {"123456","abcdef"};
        IUserManager um = new UserManager();

        /*
         * Testing regestration:
         * regestration of two different usernames returns two different IDs
         * second regestration with a username returns -1
         */
        [TestMethod]
        public void registrationReturnsDiffIDTest()
        {
            int id1 = um.register(userNames[0], passwords[0],emails[0]);
            int id2 = um.register(userNames[1], passwords[1], emails[1]);
            Assert.AreNotEqual(id1, id2);
        }

        [TestMethod]
        public void registrationAgainFailTest()
        {
            int id3 = um.register(userNames[0], passwords[0], emails[0]);
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
        public void registerLoginLogoutTest()
        {
            int id1 = um.register(userNames[3], passwords[3], emails[3]);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), id1);
            Assert.IsTrue(um.logout(id1));
        }

        [TestMethod]
        public void loginTwiceFailTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);
            Assert.IsTrue(um.logout(id1));
        }

        [TestMethod]
        public void loginWithoutRegisterTest()
        {
            Assert.AreEqual(um.login(userNames[4], passwords[4]), -1);
        }

        [TestMethod]
        public void logoutTwiceFailsTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            Assert.IsTrue(um.logout(id1));
            Assert.IsFalse(um.logout(id1));
        }


        /*
         * Testing change username:
         * checking that the username changes
         */
        [TestMethod]
        public void changeUsernameNotLogedinTest()
        {
            int id1 = -1;
            id1 = um.changeUsername(id1, userNames[0], userNames[1]);
            Assert.AreEqual(id1, -1); //should be logedin
        }

        [TestMethod]
        public void changeUsernameTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changeUsername(id1, userNames[0], userNames[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(userNames[1].CompareTo(um.getUsername(id1)), 0);
            id1 = um.changeUsername(id1, userNames[1], userNames[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getUsername(id1)), 0);
            Assert.IsTrue(um.logout(id1));
        }

        [TestMethod]
        public void changeUsernameIncorrectDetailsTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.changeUsername(id1, userNames[1], userNames[0]), -1);//user details incorrect
            Assert.IsTrue(um.logout(id1));
        }

        /*
         * Testing change password:
         * checking that the password changes
         */
        [TestMethod]
        public void changePasswordNotLogedinTest()
        {
            int id1 = -1;
            id1 = um.changePassword(id1, passwords[0], passwords[1]);
            Assert.AreEqual(id1, -1); //should be logedin
        }

        [TestMethod]
        public void changePasswordTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changePassword(id1, passwords[0], passwords[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(passwords[1].CompareTo(um.getPassword(id1)), 0);
            id1 = um.changePassword(id1, passwords[1], passwords[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getPassword(id1)), 0);
            Assert.IsTrue(um.logout(id1));
        }

        [TestMethod]
        public void changePasswordIncorrectDetailsTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.changeUsername(id1, passwords[1], passwords[0]), -1);//user details incorrect
            Assert.IsTrue(um.logout(id1));
        }

        /*
         * Testing add friend:
         * checking that friend was added after the friend approved
         */
        [TestMethod]
        public void addFriendTest()
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
        public void deactivateTest()
        {
            int id1 = um.login(userNames[0], passwords[0]);
            um.deactivate(id1);
            Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);//should fail - users not exists
        }
    }

    [TestClass]
    public class forumTest
    {
        String[] titels = { "sport","nature"};
        String[] subTitels = { "football","basketball","animals","plants"};
        IForumManager fm = ForumManager.getInstance();
        String[] user = { "tomer", "tomer@gmail.com", "123456" };

        /*
         * Testing creating forum:
         * creating forums eith two different titles returns two different IDs
         * second creation with same title returns -1
         */
        [TestMethod]
        public void creatingForumReturnsDiffIDTest()
        {
            int id1 = fm.createForum(titels[0]);
            int id2 = fm.createForum(titels[1]);
            int id3 = fm.createForum(titels[0]);
            Assert.AreNotEqual(id1, id2);
        }

        [TestMethod]
        public void creatingForumWithExistTiltleFailTest()
        {
            int id3 = fm.createForum(titels[0]);
            Assert.AreEqual(id3, -1);
        }

        /*
          * Testing creating forum:
          * creating subforums with two different topics in same forum returns two different IDs
          * second creation with same topic in same forum returns -1
          */
        [TestMethod]
        public void creatingSubForumReturnsDiffIDTest()
        {
            int id1 = fm.getForumId(titels[0]);
            int id2 = fm.createSubForum(subTitels[1], id1);
            int id3 = fm.createSubForum(subTitels[0], id1);
            Assert.AreNotEqual(id1, id2);
        }

        [TestMethod]
        public void creatingSubForumWithExistTiltleFailTest()
        {
            int id1 = fm.getForumId(titels[0]);
            int id3 = fm.createSubForum(subTitels[0], id1);
            Assert.AreEqual(id3, -1);
        }

        /*
         * testing adding and removal of admin to forum
         */
        [TestMethod]
        public void addAdminTest()
        {
            int id1 = fm.getForumId(titels[0]);
            int userId = fm.register(user[0], user[1], user[2], id1);
            fm.addAdmin(userId, id1);
            Assert.IsTrue(fm.isAdmin(userId,id1));
            fm.removeAdmin(userId, id1);
            fm.unRegister(userId, id1);
        }

        [TestMethod]
        public void removeAdminTest()
        {
            int id1 = fm.getForumId(titels[0]);
            int userId = fm.register(user[0], user[1], user[2], id1);
            Assert.IsFalse(fm.isAdmin(userId, id1));
            fm.addAdmin(userId, id1);
            fm.removeAdmin(userId, id1);
            Assert.IsFalse(fm.isAdmin(userId, id1));
            fm.unRegister(userId, id1);
        }

        /*
         * testing adding and removal of moderator to subforum
         */
        [TestMethod]
        public void addModeratorTest()
        {
            int forumId = fm.getForumId(titels[0]);
            int subForumId = fm.getSubForumId(forumId, subTitels[0]);
            int userId = fm.register(user[0], user[1], user[2], forumId);
            fm.addModerator(userId, forumId, subForumId);
            Assert.IsTrue(fm.isModerator(userId, forumId, subForumId));
            fm.removeModerator(userId, forumId, subForumId);
            fm.unRegister(userId, forumId);
        }

        [TestMethod]
        public void removeModeratorTest()
        {
            int forumId = fm.getForumId(titels[0]);
            int subForumId = fm.getSubForumId(forumId, subTitels[0]);
            int userId = fm.register(user[0], user[1], user[2], forumId);
            Assert.IsFalse(fm.isModerator(userId, forumId, subForumId));
            fm.addModerator(userId, forumId, subForumId);
            fm.removeModerator(userId, forumId, subForumId);
            Assert.IsFalse(fm.isModerator(userId, forumId, subForumId));
            fm.unRegister(userId, id1);
        }
    }
}
