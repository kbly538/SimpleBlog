using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBlog.Data;
using SimpleBlog.Data.FileManager;
using SimpleBlog.Data.Repository;



namespace SimpleBlog
{
	public class Startup
	{

		private IConfiguration _config;
		public Startup(IConfiguration configuration)
		{
			_config = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireLowercase = false;
			}
				

			)
				//.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Auth/Login";
			});
			services.AddMvc(option => option.EnableEndpointRouting = false);
			services.AddTransient<IRepository, Repository>();			
			services.AddTransient<IFileManager, FileManager>();			


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();



			
		}
	}
}
