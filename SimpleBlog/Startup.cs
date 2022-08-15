using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBlog.Configuration;
using SimpleBlog.Data;
using SimpleBlog.Data.FileManager;
using SimpleBlog.Data.Repository;
using SimpleBlog.Services;
using SimpleBlog.Services.Email;
using SimpleBlog.Util;

namespace SimpleBlog
{
	public class Startup
	{

		private readonly IConfiguration _config;
		private string _connectionString;
		public Startup(IConfiguration configuration)
		{
			_config = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			_connectionString = _config.GetConnectionString("DefaultConnection");

			services.Configure<SmtpSettings>(_config.GetSection("SmtpSettings"));


			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_connectionString));

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
			//services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


			services.AddTransient<IRepository, Repository>();			
			services.AddTransient<IFileManager, FileManager>();
			services.AddSingleton<IEmailService, EmailServiceImpl>();


			services.AddMvc(option =>
			{

				option.EnableEndpointRouting = false;
				option.CacheProfiles.Add("Monthly",
					new CacheProfile { Duration = (int) CacheProfileDurations.Monthly});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				
			}
			app.UseDeveloperExceptionPage();

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();



			
		}
	}
}
