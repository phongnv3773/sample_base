using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ss.Data.Models;
using Ss.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ServerAPI.App_Start
{
    public class OptionOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private const string LoginTypeKey = "login_type";
        private const string ClientTypeKey = "client_type";
        private const string HeaderKey = "Access-Control-Allow-Origin";
        private readonly IRepository<User> _repository;

        public OptionOAuthAuthorizationServerProvider(IRepository<User> repository)
        {
            _repository = repository;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.OwinContext.Set(LoginTypeKey, context.Parameters.Get(LoginTypeKey));
            context.OwinContext.Set(ClientTypeKey, context.Parameters.Get(ClientTypeKey));
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = _repository.Get().FirstOrDefault(o => o.UserName.Equals(context.UserName) && o.Password.Equals(context.Password));
            if (user == null)
            {
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Permission.ToString()));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "user_id", user.Id.ToString()
                },
                {
                    "user_name", user.UserName ?? string.Empty
                },
                {
                    "permission", user.Permission.ToString()
                },
                {
                    "full_name", user.FullName
                }
            });

            Startup.OAuthServerOptions.AccessTokenExpireTimeSpan = TimeSpan.FromDays(1);
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}