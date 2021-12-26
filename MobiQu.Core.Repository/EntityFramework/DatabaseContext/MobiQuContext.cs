using Microsoft.EntityFrameworkCore;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Domain.Entitites.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Core.Domain.DatabaseContext
{
    public class MobiQuContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MobiQu;User Id=SA;Password:Qazxsw+123;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MobiQu;");
        }

        public DbSet<SmartBox> SmartBox { get; set; }
        public DbSet<ColdChainBox> ColdChainBox { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<MobiQuBranchTableSettings> MobiQuBranchTableSettings { get; set; }
    }
}
