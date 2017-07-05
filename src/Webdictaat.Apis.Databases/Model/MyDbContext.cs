using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webs2_api.Model;

namespace Webdictaat.Apis.Databases.Model
{

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>()
                .HasKey(c => new { c.Email, c.AssignmentId });
        }

        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}
