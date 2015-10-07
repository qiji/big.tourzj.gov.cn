using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace BigFile.DAL
{
    public class BFEntities : DbContext
    {
        public BFEntities()
            : base("BFDBConnectionString")
        {

        }

        public DbSet<ManageUser> ManageUsers { get; set; }
        public DbSet<BFSysSet> BFSysSets { get; set; }

        public DbSet<BFInfo> BFInfos { get; set; }
    }
}
