using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Message;
using Forum;

namespace testProject
{
    [TestClass]
    public class UseCasesTest
    {
        String[] titels = { "sport", "nature" };
        String[] subTitels = { "football", "basketball", "animals", "plants" };
        String[] topic = { "man u", "juve" };
        String[] body = { "best team in the world" };
        String[] userNames = { "tomer.b", "tomer.s", "gal.b", "gal.p", "osher" };
        String[] emails = { "tomer.b@gmail.com", "tomer.s@gmail.com", "gal.b@gmail.com", "gal.p@gmail.com", "osher@gmail.com" };
        String[] passwords = { "Ab3","123456", "abcdef" };
        String[] superAdmin = { "admin", "admin" };
        IMessageManager mm = MessageManager.Instance();
        IForumManager fm = ForumManager.getInstance();

        /*
         * use case - forum creation
         */

        [TestMethod]
        public void forumCreationTest()
        {
            int id1 = fm.createForum(titels[0]);
            Assert.AreNotEqual(id1, -1);
            fm.removeForum(id1);
        }

        [TestMethod]
        public void forumCreationFailTest()
        {
            int id1 = fm.createForum(titels[0]);
            int id3 = fm.createForum(titels[0]);
            Assert.AreEqual(id3, -1);
            fm.removeForum(id1);
        }

        /*
         * use case - set policy
         */
        [TestMethod]
        public void setPolicyTest()
        {
            int id1 = fm.createForum(titels[0]);
            fm.setPolicy(4, "", true, true, true, false, 3, id1);
            Assert.AreNotEqual(-1,fm.register(userNames[0], passwords[0], emails[0],id1));//password valid
            fm.removeForum(id1);
        }
        [TestMethod]
        public void setPolicyIncorrectPasswordTest()
        {
            int id1 = fm.createForum(titels[0]);
            fm.setPolicy(4, "", true, true, true, false, 3, id1);
            Assert.AreEqual(-1, fm.register(userNames[0], passwords[1], emails[0], id1));//password only lowercase
            fm.removeForum(id1);
        }

        /*
         * use case - user registration
         */
        [TestMethod]
        public void userRegistrationToForumTest()
        {
            int id1 = fm.createForum(titels[0]);
            Assert.AreNotEqual(-1, fm.register(userNames[0], passwords[0], emails[0], id1));
            fm.removeForum(id1);
        }

        /*
         * use case - user login
         */
        [TestMethod]
        public void userLoginForumTest()
        {
            int id1 = fm.createForum(titels[0]);
            int userId = fm.register(userNames[0], passwords[0], emails[0], id1);
            Assert.AreEqual(userId, fm.login(userNames[0], passwords[0], id1));
            fm.removeForum(id1);
        }

        /*
         * use case - user logout
         */
        [TestMethod]
        public void userLogoutForumTest()
        {
            int id1 = fm.createForum(titels[0]);
            int userId = fm.register(userNames[0], passwords[0], emails[0], id1);
            userId = fm.login(userNames[0], passwords[0], id1);
            Assert.IsTrue(fm.logout(userId,id1));
            fm.removeForum(id1);
        }

        [TestMethod]
        public void userLogoutNotLogedInForumTest()
        {
            int id1 = fm.createForum(titels[0]);
            int userId = fm.register(userNames[0], passwords[0], emails[0], id1);
            Assert.IsFalse(fm.logout(userId, id1));
            fm.removeForum(id1);
        }

        /*
         * use case - creation of sub forum
         */
        [TestMethod]
        public void subForumCreationTest()
        {
            int id1 = fm.createForum(titels[0]);
            int userId = fm.register(userNames[0], passwords[0], emails[0], id1);
            fm.addAdmin(userId, id1);
            Assert.AreNotEqual(-1, fm.createSubForum(subTitels[0], id1, userId));
            fm.removeForum(id1);
        }

        [TestMethod]
        public void subForumCreationUserNoAdminTest()
        {
            int id1 = fm.createForum(titels[0]);
            int userId = fm.register(userNames[0], passwords[0], emails[0], id1);
            Assert.AreEqual(-1, fm.createSubForum(subTitels[0], id1, userId));
            fm.removeForum(id1);
        }
    }
}
