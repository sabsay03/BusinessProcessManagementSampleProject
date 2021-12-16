using EntityLayer.Concrete;
using EntityLayer.Concrete.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("server=LAPTOP-DACKTG5J\\SQLEXPRESS;database=BpmDb; integrated security=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Mission> Tasks { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.GroupId, gm.MemberId });
            modelBuilder.Entity<GroupMember>()
                .HasOne<User>(gm => gm.Member)
                .WithMany(b => b.GroupMembers)
                .HasForeignKey(gm => gm.MemberId);
            modelBuilder.Entity<GroupMember>()
                .HasOne<Group>(gm => gm.Group)
                .WithMany(c => c.GroupMembers)
                .HasForeignKey(gm => gm.GroupId);

            modelBuilder.Entity<ProjectMember>()
            .HasKey(pm => new { pm.ProjecId, pm.MemberId });
            modelBuilder.Entity<ProjectMember>()
                .HasOne<User>(pm => pm.Member)
                .WithMany(b => b.ProjectMembers)
                .HasForeignKey(pm => pm.MemberId);
            modelBuilder.Entity<ProjectMember>()
                .HasOne<Project>(pm => pm.Project)
                .WithMany(c => c.ProjectMembers)
                .HasForeignKey(pm => pm.ProjecId);

            modelBuilder.Entity<Mission>()
           .HasOne<User>(s => s.Member)
           .WithMany(g => g.Tasks)
           .HasForeignKey(s => s.MemberId);
           
        }

    }
}
