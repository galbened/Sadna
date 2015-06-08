using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPagingAngularClient.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class ClubPage
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }

    public class ClubQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ClubQuery1
    {
        public string Name { get; set; }        
    }

    public class Forum
    {
        public int Id { get; set; }
        public string title { get; set; }
        public List<SubForum> SubForums;
    }

    public class SubForum
    {
        public int Id { get; set; }
        public string title { get; set; }
        public List<Thread> Threads;

    }

    public class Thread
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public List<Comment> Comments;
        public string date { get; set; }
        public string publisher { get; set; }
    }

    public class Comment
    {
        public int Id { get; set; }
        public string topic { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public string publisher { get; set; }
    }

    public class signupParams
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}