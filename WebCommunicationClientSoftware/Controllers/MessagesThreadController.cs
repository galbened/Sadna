using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;
using Driver;


namespace WebCommunicationClientSoftware.Controllers
{
    public class MessagesThreadController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

        public List<ThreadInfo> Get(int forumId, int subforumId)
        {
            List<ThreadInfo> result = driver.GetAllThreads(forumId, subforumId);
            return result;
        }

        public WebCommunicationClientSoftware.Models.userData Post(WebCommunicationClientSoftware.Models.loginParams pars)
        {
            int userId = driver.Login(pars.username, pars.password, pars.forumId);
            string userType = driver.GetUserType(pars.forumId, userId);
            WebCommunicationClientSoftware.Models.userData UD = new WebCommunicationClientSoftware.Models.userData();
            UD.type = userType;
            UD.userId = userId;
            UD.userName = pars.username;
            return UD;
        }

    }
}