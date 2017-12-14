using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Session
{
    public class ClaimsUserSession : IUserSession
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IAuthenticationSchemeProvider schemes;
        private readonly IAuthenticationHandlerProvider handlers;
        private readonly ILogger logger;

        private HttpContext HttpContext => httpContextAccessor.HttpContext;

        private ClaimsPrincipal principal;
        private AuthenticationProperties properties;


        public ClaimsUserSession(IHttpContextAccessor httpContextAccessor, IAuthenticationSchemeProvider schemes, IAuthenticationHandlerProvider handlers, ILogger<IUserSession> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.schemes = schemes;
            this.handlers = handlers;
            this.logger = logger;
        }


        private async Task<string> GetCookieSchemeAsync()
        {
            var defaultScheme = await schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultScheme == null)
            {
                throw new InvalidOperationException("No DefaultAuthenticateScheme found.");
            }

            return defaultScheme.Name;
        }

        private async Task AuthenticateAsync()
        {
            if (principal == null || properties == null)
            {
                var scheme = await GetCookieSchemeAsync();
                var handler = await handlers.GetHandlerAsync(HttpContext, scheme);

                if (handler == null)
                {
                    throw new InvalidOperationException($"No authentication handler is configured to authenticate for the scheme: {scheme}");
                }

                var result = await handler.AuthenticateAsync();
                if (result != null && result.Succeeded)
                {
                    principal = result.Principal;
                    properties = result.Properties;
                }
            }
        }

        public async Task<string> GetUserIdAsync()
        {
            await AuthenticateAsync();

            return principal?.Identity?.Name;
        }
    }
}
