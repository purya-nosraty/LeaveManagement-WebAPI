using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Middlewares;
public class GlobalExceptionHandelrMiddleware(RequestDelegate next)
{
	private RequestDelegate Next { get; } = next;


	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await Next(httpContext);
		}
		catch (Exception)
		{
			//Todo
			//httpContext.Response.Redirect
			//	(location: "/Errors/Error", permanent: false);
		}
	}
}