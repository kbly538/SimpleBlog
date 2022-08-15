using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;
using SimpleBlog.Models.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}


		public DbSet<Post>	Post {get; set;}
		public DbSet<MainComment> MainComment { get; set; }
		public DbSet<SubComment> SubComment { get; set; }
	}
}
