using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using assignment5.Models;

//ACCESS TO DATABASE
namespace assignment5.DAL
{
    public class RequestContext : DbContext
    {
        public RequestContext() : base("name=OurRequests")
        {

        }

        public virtual DbSet<Request> Request { get; set; } 
    }
}