using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Tiresias.AuthConfig))]

namespace Tiresias
{
    public class AuthConfig
    {
        public void Configuration(IAppBuilder app)
        {
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieSecure = CookieSecureOption.SameAsRequest
            });
        }
    }
}
