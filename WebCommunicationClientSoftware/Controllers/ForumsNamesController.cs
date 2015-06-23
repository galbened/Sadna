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
    [RoutePrefix("api/forumsnames")]
    public class ForumsNamesController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

         public ForumsNamesController()
        {
        }

        [Route("getForumsNames")]
        public List<string> Get()
        {
            List<string> names = driver.GetForumTopics();
            return names;
        }

        [Route("getSubForumsNames/{forumId:int}")]
        public List<string> Get(int forumId)
        {
            List<string> names = driver.GetSubForumsTopics(forumId);
            return names;
        }
    }
    
}