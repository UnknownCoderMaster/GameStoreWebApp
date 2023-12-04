using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using GameStoreWebApp.Service.Exceptions;

namespace GameStoreWebApp.API.Middlewares
{
	public class GameStoreExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<GameStoreExceptionMiddleware> logger;
		public GameStoreExceptionMiddleware(RequestDelegate next, ILogger<GameStoreExceptionMiddleware> logger)
		{
			this.next = next;
			this.logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{

			try
			{
				await next.Invoke(context);
			}
			catch (GameAppException ex)
			{
				await HandleException(context, ex.Code, ex.Message);
			}
			catch (Exception ex)
			{
				//Log
				logger.LogError(ex.ToString());

				await HandleException(context, 500, ex.Message);
			}
		}

		public async Task HandleException(HttpContext context, int code, string message)
		{
			context.Response.StatusCode = code;
			await context.Response.WriteAsJsonAsync(new
			{
				Code = code,
				Message = message
			});
		}
	}
}
