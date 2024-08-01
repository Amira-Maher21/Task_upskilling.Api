using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upskilling.Domain.Models;

namespace upskilling.Repository.Data
{
	public class upskillingDbContext :DbContext
	{
		public upskillingDbContext(DbContextOptions<upskillingDbContext> options) : base(options)
		{

		}
		public upskillingDbContext()
		{

		}

		public DbSet<TeamMember> TeamMembers { get; set; }
		public DbSet<Tasks> Tasks { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=DESKTOP-2I0OH7I\\SQLEXPRESS;Initial Catalog=upskillingApi; User Id=sa;Password=123;TrustServerCertificate=true");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure the primary keys
			modelBuilder.Entity<TeamMember>().HasKey(tm => tm.MemberId);
			modelBuilder.Entity<Tasks>().HasKey(t => t.TaskId);

			// Configure the relationships
			modelBuilder.Entity<TeamMember>()
				.HasMany(tm => tm.Tasks)
				.WithOne(t => t.TeamMember)
				.HasForeignKey(t => t.MemberId);

			 

			base.OnModelCreating(modelBuilder);
		}
	}
 }

