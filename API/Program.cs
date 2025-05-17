using System;
using System.Text;
using Infrastructure.Data;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API;

internal static class Program
{
	static Program()
	{
	}

	private static async Task Main()
	{
		var webApplication = new WebApplicationOptions
		{
			EnvironmentName = Environments.Development
			//EnvironmentName = Environments.Production
		};

		var builder = WebApplication.CreateBuilder(options: webApplication);


		// Add services to the container.
		builder.Services.AddControllers();

		// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddOpenApi();


		builder.Services.Configure
			<RequestLocalizationOptions>(option =>
			{
				var supportedCultures = new[]
				{
				new CultureInfo(name: "fa-IR"),
				new CultureInfo(name: "en-US"),
			};

				option.SupportedCultures = supportedCultures;
				option.SupportedUICultures = supportedCultures;

				option.DefaultRequestCulture =
					new RequestCulture(culture: "en-US", uiCulture: "en-US");
			});


		builder.Services.AddDbContext<AppDbContext>(option =>
			option.UseSqlServer(builder.Configuration
				.GetConnectionString(name: nameof(Domain.Shared.Utility.Const.DefaultConnection))
				)
			)
		;


		builder.Services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],

					ValidateAudience = true,
					ValidAudience = builder.Configuration["Jwt:Audience"],

					ValidateIssuerSigningKey = true,

					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)
					),

					ValidateLifetime = true,

					ClockSkew = TimeSpan.Zero
				};

				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						return Task.CompletedTask;
					}
				};
			})
		;


		var app = builder.Build();


		//using (var scope = app.Services.CreateScope())
		//{
		//	var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		//	await appDbContext.Database.MigrateAsync();
		//}

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.MapOpenApi();
		}
		else
		{
			//app.UseExceptionHandler("/Errors/Error");
			app.UseHsts();
		}


		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();


		app.MapControllers();


		await app.RunAsync();
	}
}
