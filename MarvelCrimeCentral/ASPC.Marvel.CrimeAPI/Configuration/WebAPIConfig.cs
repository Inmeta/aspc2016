using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Metadata;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Xml;

namespace ASPC.Marvel.CrimeAPI
{
    public static class WebAPIConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Node>("Node");
            builder.EntitySet<Crime>("Crime");
            builder.EntitySet<Agent>("Agent");

            //config
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API routes
            config.Routes.MapHttpRoute("api", "api/{controller}/{action}/{id}", new { action = RouteParameter.Optional, id = RouteParameter.Optional });

            config.Filters.Add(new AuthorizeAttribute());
            //config.MessageHandlers.Add(new TokenValidationHandler());
        }
    }

    internal class TokenValidationHandler : DelegatingHandler
    {
        static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = string.Empty;
            IEnumerable<string> authzHeaders;

            if ((!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)) { return false; }

            var bearerToken = authzHeaders.ElementAt(0);

            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string tokenRaw = string.Empty;

            try
            {
                if (!TryRetrieveToken(request, out tokenRaw)) { return base.SendAsync(request, cancellationToken); }

                var validationParameters = new TokenValidationParameters()
                {

                    ValidIssuer = SecurityHelper.CertificateValidIssuer,
                    ValidAudience = SecurityHelper.CertificateValidAudience,
                    IssuerSigningToken = new X509SecurityToken(SecurityHelper.GetCertificate()),
                    ValidateLifetime = false,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    //ClockSkew = new TimeSpan(40, 0, 0)
                };

                SecurityToken token = new JwtSecurityToken();
                ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(tokenRaw, validationParameters, out token);

                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null) { HttpContext.Current.User = Thread.CurrentPrincipal; }

            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}