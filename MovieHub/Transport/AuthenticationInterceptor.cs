using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using MovieHub.Utilities.Services;
using System.Security.Claims;

namespace MovieHub.Transport
{
    public class AuthenticationInterceptor : DefaultHttpRequestInterceptor
    {
        public override async ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
        {
            requestBuilder.SetGlobalState("user", null);
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var authService = context.RequestServices.GetService<IAuthenticationService>();
            if (token != null)
            {
                var result = await authService.ValidateTokenAsync(context, token);

                if (result != null)
                {
                    var identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("sub", result.Claims.FirstOrDefault(x => x.ValueType == ClaimTypes.NameIdentifier)?.Value ?? "")
                    },
                    "Bearer");

                    context.User.AddIdentity(identity);
                    requestBuilder.SetGlobalState("user", result.Name);

                    await base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
                }
            }

            context.Response.StatusCode = 401;
            await base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
        }
    }
}
