using Messanger.Api.Security;
using Messanger.Infra.DataContexts;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace Messanger.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            ConfigureWebApi(configuration);
            ConfigureOAuth(app, new MessangerDbContext());

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(configuration);
        }

        public void ConfigureWebApi(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void ConfigureOAuth(IAppBuilder app, MessangerDbContext dataContext)
        {
            OAuthAuthorizationServerOptions authorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/account/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationServerProvider(dataContext)
            };
            app.UseOAuthAuthorizationServer(authorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}