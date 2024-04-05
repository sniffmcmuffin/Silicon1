using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace Silicon1.Helpers.Middlewares;

public static class ApplicationBuilderExtentions
{
	public static IApplicationBuilder UseUserSessionValidation(this IApplicationBuilder builder )
	{
		return builder.UseMiddleware<UserSessionValidationMiddleware>();
	}
}
