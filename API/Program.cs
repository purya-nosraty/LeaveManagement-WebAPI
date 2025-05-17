using Domain.Shared;
using Infrastructure.Data;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
				.GetConnectionString(name: nameof(Utility.Const.DefaultConnection))));


		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Errors/Error");
			app.UseHsts();
		}

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
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
