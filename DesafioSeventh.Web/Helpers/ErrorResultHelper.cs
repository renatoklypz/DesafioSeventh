using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace DesafioSeventh.Web.Helpers
{
	public static class ErrorResultHelper
	{
		private static Dictionary<string, HttpStatusCode> errorCodes = new Dictionary<string, HttpStatusCode>()
		{
			{"err_conflict", HttpStatusCode.Conflict },
			{"err_not_exists", HttpStatusCode.NotFound },
			{"err_validation", HttpStatusCode.BadRequest },
			{"vid_invalid_file", HttpStatusCode.BadRequest }
		};

		public static void UseErrorMap(this IApplicationBuilder app, Dictionary<string, HttpStatusCode>? errorCodes = null)
		{
			app.UseExceptionHandler(a => a.Run(OnException));
			if (errorCodes != null)
			{
				foreach (var item in errorCodes)
				{
					if (ErrorResultHelper.errorCodes.ContainsKey(item.Key))
					{
						ErrorResultHelper.errorCodes[item.Key] = item.Value;
					}
					else
					{
						ErrorResultHelper.errorCodes.Add(item.Key, item.Value);
					}
				}
			}

		}

		public static async Task OnException(HttpContext context)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";
			var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

			if (null != exceptionObject)
			{
				static WebException GetException(object obj)
				{
					string[] eProps = new string[3] { "message", "report", "code" };

					Dictionary<string, PropertyInfo> props = obj.GetType().GetProperties().Where(p => eProps.Any(a => a == p.Name.ToLower()))
						.ToDictionary(s => s.Name.ToLower(), s => s);

					var result = new WebException
					{
						Message = props.ContainsKey("message") ? (string?)props["message"].GetValue(obj) : null,
						Report = props.ContainsKey("report") ? props["report"].GetValue(obj) : null,
						Code = props.ContainsKey("code") ? (string?)props["code"].GetValue(obj) : null
					};

					return result;
				}

				Exception ex = exceptionObject.Error;
				var resultObj = GetException(ex);

				if (resultObj.Code != null && errorCodes.ContainsKey(resultObj.Code))
				{
					context.Response.StatusCode = (int)errorCodes[resultObj.Code];
				}

				string resultJson = JsonConvert.SerializeObject(resultObj, SetupHelper.JSONFormatSetting);

				await context.Response.WriteAsync(resultJson).ConfigureAwait(false);
			}
		}
	}
}
