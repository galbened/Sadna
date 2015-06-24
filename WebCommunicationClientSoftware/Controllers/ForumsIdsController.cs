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
    [RoutePrefix("api/forumsids")]
    public class ForumsIdsController : ApiController
    {
        private static IApplicationBridge driver = BridgeReal.GetInstance();

        public ForumsIdsController()
        {
        }
        // GET: api/forums/getForumsIds
        //[Route("getForumsIds")]
        public List<int> Get()
        {
            List<int> ids = driver.GetForumIds();
            return ids;
        }

        public int Post(WebCommunicationClientSoftware.Models.newCommentParams comment)
        {
            return driver.Comment(comment.firstMessageId, comment.publisherID, comment.publisherName, comment.title, comment.body);
        }
        
      
    }
}
