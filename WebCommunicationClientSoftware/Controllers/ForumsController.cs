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
    [RoutePrefix("api/forums")]
    public class ForumsController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

        public ForumsController()
        {
        }
        // GET: api/forums/getForumsIds
        [Route("getForumsIds")]
        public List<int> Get()
        {
            List<int> ids = driver.GetForumIds();
            return ids;
        }

        [Route("getSubForumsIds/{forumId:int}")]
        public List<int> Get(int forumId)
        {
            List<int> ids = driver.GetSubForumsIds(forumId);
            return ids;
        }

    }
}
