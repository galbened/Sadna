using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using User;
using Forum;
using Message;

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
            um.deactivate(id1);
            um.deactivate(id2);
        }

        [TestMethod]
        public void registrationAgainFailTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            int id3 = um.register(userNames[0], passwords[0], emails[0]);
            Assert.AreEqual(id3, -1);
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
            Assert.AreEqual(um.login(userNames[0], passwords[0]), -1);
            um.deactivate(id1);
        }

        [TestMethod]
        public void loginWithoutRegisterTest()
        {
            Assert.AreEqual(um.login(userNames[4], passwords[1]), -1);
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
        public void changeUsernameNotLogedinTest()
        {
            int id1 = -1;
            id1 = um.changeUsername(id1, userNames[0], userNames[1]);
            Assert.AreEqual(id1, -1); //should be logedin
        }

        [TestMethod]
        public void changeUsernameTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changeUsername(id1, userNames[0], userNames[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(userNames[1].CompareTo(um.getUsername(id1)), 0);
            id1 = um.changeUsername(id1, userNames[1], userNames[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getUsername(id1)), 0);
            um.deactivate(id1);
        }

        [TestMethod]
        public void changeUsernameIncorrectDetailsTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            Assert.AreEqual(um.changeUsername(id1, userNames[1], userNames[0]), -1);//user details incorrect
            um.deactivate(id1);
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
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
            int id2 = um.changePassword(id1, passwords[0], passwords[1]);
            Assert.AreEqual(id1, id2); //should return the same ID
            Assert.AreEqual(passwords[1].CompareTo(um.getPassword(id1)), 0);
            id1 = um.changePassword(id1, passwords[1], passwords[0]);
            Assert.AreEqual(userNames[0].CompareTo(um.getPassword(id1)), 0);
            um.deactivate(id1);
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
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            int id2 = um.register(userNames[1], passwords[1], emails[1]);
            id1 = um.login(userNames[0], passwords[0]);
            int notID = um.addFriend(id1, id2);
            Assert.AreNotEqual(notID, -1); //notification should be sent
            Assert.AreEqual(um.addFriend(id1,id2),-1);//notification shouldn't be sent
            um.deactivate(id1);
            um.deactivate(id2);
        }

        /*
         * Testing deactivate account:
         * checking that the user account was diactivated
         */
        [TestMethod]
        public void deactivateTest()
        {
            int id1 = um.register(userNames[0], passwords[0], emails[0]);
            id1 = um.login(userNames[0], passwords[0]);
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
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateForum(titels[1]);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(id1);
            fm.RemoveForum(id2);
        }

        [TestMethod]
        public void creatingForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id3 = fm.CreateForum(titels[0]);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
        }

        /*
          * Testing creating forum:
          * creating subforums with two different topics in same forum returns two different IDs
          * second creation with same topic in same forum returns -1
          */
        [TestMethod]
        public void creatingSubForumReturnsDiffIDTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateSubForum(subTitels[1], id1, 1);
            int id3 = fm.CreateSubForum(subTitels[0], id1, 1);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(id1);
            fm.RemoveSubForum(id1, id2, 1);
            fm.RemoveSubForum(id1, id3, 1);
        }

        [TestMethod]
        public void creatingSubForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int id2 = fm.CreateSubForum(subTitels[0], id1, 1);
            int id3 = fm.CreateSubForum(subTitels[0], id1, 1);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
            fm.RemoveSubForum(id1, id2, 1);
        }

        /*
         * testing adding and removal of admin to forum
         */
        [TestMethod]
        public void addAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            fm.AddAdmin(userId, id1);
            Assert.IsTrue(fm.IsAdmin(userId,id1));
            fm.RemoveAdmin(userId, id1);
            fm.UnRegister(userId, id1);
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void removeAdminTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.AddAdmin(userId, id1);
            fm.RemoveAdmin(userId, id1);
            Assert.IsFalse(fm.IsAdmin(userId, id1));
            fm.UnRegister(userId, id1);
            fm.RemoveForum(id1);
        }

        /*
         * testing adding and removal of moderator to subforum
         */
        [TestMethod]
        public void addModeratorTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int subForumId = fm.CreateSubForum(subTitels[0],forumId, 1);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Console.WriteLine(subForumId);
            fm.AddModerator(userId, forumId, subForumId, 1);
            Assert.IsTrue(fm.IsModerator(userId, forumId, subForumId));
            fm.RemoveModerator(userId, forumId, subForumId);
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(forumId, subForumId, 1);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void removeModeratorTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, 1);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.AddModerator(userId, forumId, subForumId, 1);
            fm.RemoveModerator(userId, forumId, subForumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.UnRegister(userId, forumId);
            fm.RemoveSubForum(forumId, subForumId, 1);
            fm.RemoveForum(forumId);
        }
    }

    [TestClass]
    public class messageTest
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };   
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "123456", "abcdef" };
        //IMessageManager mm = MessageManager.getInstance();
        IForumManager fm = ForumManager.getInstance();
    }

}
