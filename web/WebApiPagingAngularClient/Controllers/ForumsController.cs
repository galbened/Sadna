using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiPagingAngularClient.Models;
using WebApiPagingAngularClient.Utility;

namespace WebApiPagingAngularClient.Controllers
{
    [RoutePrefix("api/forums")]
    public class ForumsController : ApiController
    {

        // GET: api/forums/getForums
        [Route("getForums")]
        public IHttpActionResult Get()
            {
                var data =  new List<Forum>()
                {
                    new Forum{
                        Id = 1000,
                        title = "sport",
                        },
                    new Forum{
                        Id= 1001,
                        title= "nature",
                        }
                };
            
                var result = new
                {
                    data = data
                };

                return Ok(result);
        }
        // GET: api/forums/getForum/forumId
        [Route("getForum/{forumId:int}")]
        public IHttpActionResult Get(int forumId)
        {
            if (forumId == 1000)
            {
                var data = new Forum
                {
                    Id = 1000,
                    title = "sport",
                    SubForums = new List<SubForum>()
                            {
                            new SubForum{
                                Id = 100,
                                title = "football",
                                },
                            new SubForum{
                                Id =  101,
                                title = "basketball",
                                }
                            }
                };
                var result = new
                {
                    data = data
                };

                return Ok(result);
            }
            else
            {
                var data = new Forum
                {
                    Id = 1001,
                    title = "nature",
                    SubForums = new List<SubForum>()
                                    {
                                        new SubForum{
                                            Id= 100,
                                            title= "animals",
                                        },
                          
                                        new SubForum{
                                            Id= 101,
                                            title= "plants",
                                        }
                                }

                };
                var result = new
                {
                    data = data
                };

                return Ok(result);
            }
        }

        // GET: api/forums/getSubForum/forumId/subForumId
        [Route("getSubForum/{forumId:int}/{subForumId:int}")]
        public IHttpActionResult Get(int forumId,int subForumId)
        {
            var data =  new List<Forum>()
                {
                    new Forum{
                        Id = 1000,
                        title = "sport",
                        SubForums = new List<SubForum>()
                            {
                            new SubForum{
                                Id = 100,
                                title = "football",
                                Threads = new List<Thread>()
                                    {
                                        new Thread{
                                            Id = 1,
                                            topic = "juventus",
                                            content = "the best team in the world",
                                            Comments = new List<Comment>() 
                                                {
                                                    new Comment{
                                                        Id = 1,
                                                        topic =  "you're wrong",
                                                        content =  "man u is the best team",
                                                        date = "20/01/2015",
                                                        publisher = "osherda"
                                                    }
                                                },
                                            date =  "19/01/2015",
                                            publisher = "tomerse"
                                        }
                                    }
                                },
                            new SubForum{
                                Id =  101,
                                title = "basketball",
                                Threads = new List<Thread>()
                                    {
                                        new Thread{
                                            Id = 1,
                                            topic = "david blat",
                                            content = "the best coach",
                                            Comments = new List<Comment>()
                                                {
                                                    new Comment{
                                                        Id= 1,
                                                        topic= "you're wrong",
                                                        content= "he has the best players",
                                                        date= "19/01/2015",
                                                        publisher= "amit romem"
                                                    }
                                                },
                                            date="19/01/2015",
                                            publisher= "galbend"
                                        },
                                        new Thread{
                                            Id= 2,
                                            topic= "juventus",
                                            content= "the best team in the world",
                                            Comments = new List<Comment>()
                                                {
                                                new Comment{
                                                    Id= 1,
                                                    topic= "you're wrong",
                                                    content= "man u is the best team",
                                                    date= "19/01/2015",
                                                    publisher= "beri chakala"
                                                },
                                                new Comment{
                                                    Id= 2,
                                                    topic= "you're wrong",
                                                    content= "he has the best players",
                                                    date= "19/01/2015",
                                                    publisher= "ish im auto"
                                                }
                                           
                                            },
                                            date= "19/01/2015",
                                            publisher= "asaf loch"
                                        }
                                    }
                                }
                            }
                        },
                            new Forum{
                                Id= 1001,
                                title= "nature",
                                SubForums = new List<SubForum>()
                                    {
                                        new SubForum{
                                            Id= 100,
                                            title= "animals",
                                            Threads = new List<Thread>()
                                                {
                                                    new Thread{
                                                        Id= 1,
                                                        topic= "turtles",
                                                        content= "i like turtles",
                                                        Comments = new List<Comment>()
                                                            {
                                                                new Comment{
                                                                    Id =1,
                                                                    topic= "me tooooooooo",
                                                                    content= "",
                                                                    date= "19/01/2015",
                                                                    publisher= "tiki poor"
                                                                }
                                                            },
                                                        date= "19/01/2015",
                                                        publisher= "simha rif"
                                                    }
                                                }
                                        },
                          
                                    new SubForum{
                                        Id= 101,
                                        title= "plants",
                                        Threads = new List<Thread>()
                                            {
                                                new Thread{
                                                    Id= 1,
                                                    topic= "canabis",
                                                    content= "best plant",
                                                    Comments = new List<Comment>()
                                                        {
                                                            new Comment{
                                                                Id= 1,
                                                                topic= "opium too",
                                                                content= "opium is also one of the best",
                                                                date= "19/01/2015",
                                                                publisher= "galpo"
                                                            }
                                                        },
                                                    date= "19/01/2015",
                                                    publisher= "galpo"
                                                }
                                            }
                                        }
                                    }
                            }
                };
            var result = new
            {
                data = data
            };

            return Ok(result);
            
        }

    }
}
