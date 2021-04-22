using mis4200_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mis4200_Project.DAL
{
    public class CContext : DbContext 
    {
        public CContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Profile> profile { get; set; }

        //public DbSet<MyProfile> Myprofile { get; set; }
        //public DbSet<CoreValueType>CoreValueType { get; set; }
        public DbSet<Recognition>recognition { get; set; }

    }
}