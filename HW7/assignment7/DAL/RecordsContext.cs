﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using assignment7.Models;

namespace assignment7.DAL
{
    public class RecordsContext : DbContext
    {
        public RecordsContext() : base("name=OurRecords")
        {

        }

        public virtual DbSet<Record> Record { get; set; }

    }
}