using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace ScaniaTest.Middleware
{
	public class AALoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<AALoggingMiddleware> _logger;

		public AALoggingMiddleware(RequestDelegate next, ILogger<AALoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{

			string userAgent = context.Request.Headers["User-Agent"].ToString();

			var stopwatch = new Stopwatch();
			stopwatch.Start();

			try
			{
				var request = context.Request;
				var requestBodyStream = context.Request.Body;
				var requestBodyText = string.Empty;

				string[] dataType = new[] { "multipart", "image" };

				if (!(context.Request.Headers.ContentType.ToString().Contains("multipart") ||
					context.Request.Headers.ContentType.ToString().Contains("image")))
				{
					request.EnableBuffering();

					using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
					{
						requestBodyText = await reader.ReadToEndAsync();
						request.Body.Position = 0;
					}
				}
			
				await _next(context);
			
				var clientIpAddress = context.Connection.RemoteIpAddress;
				var clientIpAddress1 = context.Connection.Id;
				var clientIpAddress2 = context.Connection.LocalIpAddress;
				var clientIpAddress3 = context.Connection.LocalPort;

				stopwatch.Stop();

				_logger.LogInformation($"Request: {request.Method} {request.Path}{request.QueryString} " +
					$"\nRequest Body: {requestBodyText} " +
					$"\nElapsed Time: {stopwatch.ElapsedMilliseconds}ms" +
					$"\nClient Ip Address: {clientIpAddress}" +
					$"\nUser Agent: {userAgent}");
			}
			catch (Exception ex)
			{
				stopwatch.Stop();

				_logger.LogError(ex, $"Unhandled exception in LoggingMiddleware. Elapsed time: {stopwatch.ElapsedMilliseconds}ms");
				throw;
			}
		}

	}
	public static class AALoggingMiddlewareExtensions
	{
		public static IApplicationBuilder UseAALoggingMiddleware(this IApplicationBuilder builder)
		{
			// Add response caching middleware to the pipeline
			return builder.UseMiddleware<AALoggingMiddleware>();
		}
	}

}
