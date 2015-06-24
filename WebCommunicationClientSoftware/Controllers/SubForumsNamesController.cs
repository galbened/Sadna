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
    //[RoutePrefix("api/subforumsnames")]
    public class SubForumsNamesController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

        public List<string> Get(int forumId)
        {
            List<string> names = driver.GetSubForumsTopics(forumId);
            return names;
        }
    }
}