using EntityLayer.Concrete;
using EntityLayer.Concrete.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context: IdentityDbContext<User,Role,int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("server=LAPTOP-DACKTG5J\\SQLEXPRESS;database=BpmDb; integrated security=true");
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Mission> Tasks { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {


            modelBuilder.Entity<ProjectMember>()
            .HasKey(pm => new { pm.ProjecId, pm.MemberId });

            modelBuilder.Entity<ProjectMember>()
                .HasOne<User>(pm => pm.Member)
                .WithMany(b => b.ProjectMembers)
                .HasForeignKey(pm => pm.MemberId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectMember>()
                .HasOne<Project>(pm => pm.Project)
                .WithMany(c => c.ProjectMembers)
                .HasForeignKey(pm => pm.ProjecId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mission>()
           .HasOne<User>(s => s.Member)
           .WithMany(g => g.Tasks)
           .HasForeignKey(s => s.MemberId);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasOne(p => p.Manager).WithOne().HasForeignKey<Project>(p => p.ManagerId).OnDelete(DeleteBehavior.Restrict);

        }

    }
}
