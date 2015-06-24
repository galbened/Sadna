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
    public class SubForumsIdsController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

        public List<int> Get(int forumId)
        {
            List<int> ids = driver.GetSubForumsIds(forumId);
            return ids;
        }

    }
}