using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Axioma.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Axioma
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<SubscriptionsService>();
			services.AddScoped<ModulesService>();
			services.AddScoped<LessonsService>();
			services.AddScoped<UniversitiesService>();
			services.AddScoped<CoursesService>();
			services.AddScoped<TokenService>();
			services.AddScoped<StudentService>();
			services.AddScoped<UserService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(
					options =>
					{
						var signingKey = Convert.FromBase64String(Configuration["Jwt:SigningSecret"]);
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = false,
							ValidateAudience = false,
							ValidateIssuerSigningKey = true,
							IssuerSigningKey = new SymmetricSecurityKey(signingKey)
						};
					}
				);
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
