using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Messanger.Infra.DataContexts;
using Microsoft.Owin.Security.OAuth;

namespace Messanger.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private MessangerDbContext _dataContext;

        public AuthorizationServerProvider(MessangerDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _dataContext.Users.SingleOrDefault(u => u.Name == context.UserName && u.Password == context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Invalid credentials");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            GenericPrincipal principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            context.Validated(identity);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
    }
}