﻿using System;
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
        [ExpectedException(typeof(UsernameIsTakenException),"Username entered is taken.")]
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
        }

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
            int userId = fm.Register(user[0], user[1], user[2], id1);
            fm.AddAdmin(userId, id1);
            int id2 = fm.CreateSubForum(subTitels[1], id1, userId);
            int id3 = fm.CreateSubForum(subTitels[0], id1, userId);
            Assert.AreNotEqual(id1, id2);
            fm.RemoveForum(id1);
        }

        [TestMethod]
        public void creatingSubForumWithExistTiltleFailTest()
        {
            int id1 = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], id1);
            fm.AddAdmin(userId, id1);
            int id2 = fm.CreateSubForum(subTitels[0], id1, userId);
            int id3 = fm.CreateSubForum(subTitels[0], id1, userId);
            Assert.AreEqual(id3, -1);
            fm.RemoveForum(id1);
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
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            Console.WriteLine(subForumId);
            fm.AddModerator(userId, forumId, subForumId);
            Assert.IsTrue(fm.IsModerator(userId, forumId, subForumId));
            fm.RemoveModerator(userId, forumId, subForumId);
            fm.UnRegister(userId, forumId);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void removeModeratorTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(user[0], user[1], user[2], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.AddModerator(userId, forumId, subForumId);
            fm.RemoveModerator(userId, forumId, subForumId);
            Assert.IsFalse(fm.IsModerator(userId, forumId, subForumId));
            fm.UnRegister(userId, forumId);
            fm.RemoveForum(forumId);
        }
    }

    [TestClass]
    public class messageTest
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        String[] topic = { "man u", "juve" };
        String[] body = { "best team in the world" };
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "123456", "abcdef" };
        IMessageManager mm = MessageManager.Instance();
        IForumManager fm = ForumManager.getInstance();

        /*testing add thread
         * should succeed when title not empty
         */
        [TestMethod]
        public void addThreadTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId1 = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int threadId2 = mm.addThread(forumId, subForumId, userId, topic[1], body[0]);
            Assert.AreNotEqual(threadId1, threadId2);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void addThreadTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);         
            int threadId1 = mm.addThread(forumId, subForumId, userId, "", body[0]);
            Assert.AreEqual(threadId1, -1);
            fm.RemoveForum(forumId);
        }

        /*testing add comment
         * should succeed when title not empty
         */
        [TestMethod]
        public void addCommentTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);    
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            int commentID2 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.AreNotEqual(commentID1, commentID2);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void addCommentTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, "", body[0]);
            Assert.AreEqual(commentID1, -1);
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void addCommentThreadNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int commentID1 = mm.addComment(5, userId, topic[1], body[0]);
            Assert.AreEqual(commentID1, -2);
            fm.RemoveForum(forumId);
        }

        /*testing edit message
         * should succeed when title not empty and message ID exists
         */
        [TestMethod]
        public void editMessageTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsTrue(mm.editMessage(commentID1,topic[0], body[0]));
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void editMessageTitleEmptyTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsFalse(mm.editMessage(commentID1, "", body[0]));
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void editCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsFalse(mm.editMessage(-200, topic[0], body[0]));
            fm.RemoveForum(forumId);
        }

        /*testing delete message
         * should succeed when message ID exists
         */
        [TestMethod]
        public void deleteMessageTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsTrue(mm.deleteMessage(commentID1));
            fm.RemoveForum(forumId);
        }

        [TestMethod]
        public void deleteCommentMessageNotExistsTest()
        {
            int forumId = fm.CreateForum(titels[0]);
            int userId = fm.Register(userNames[0], passwords[0], emails[0], forumId);
            fm.AddAdmin(userId, forumId);
            int subForumId = fm.CreateSubForum(subTitels[0], forumId, userId);
            int threadId = mm.addThread(forumId, subForumId, userId, topic[0], body[0]);
            int commentID1 = mm.addComment(threadId, userId, topic[1], body[0]);
            Assert.IsFalse(mm.deleteMessage(-200));
            fm.RemoveForum(forumId);
        }
    }

}
