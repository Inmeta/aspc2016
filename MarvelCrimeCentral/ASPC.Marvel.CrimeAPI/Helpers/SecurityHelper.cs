using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace ASPC.Marvel.CrimeAPI
{
    internal class SecurityHelper
    {
        internal static readonly string CertificateThumbprint = ConfigurationManager.AppSettings.Get("CertificateThumbprint");
        internal static readonly string CertificateValidIssuer = ConfigurationManager.AppSettings.Get("CertificateValidIssuer");
        internal static readonly string CertificateValidAudience = ConfigurationManager.AppSettings.Get("CertificateValidAudience");

        internal static X509Certificate2 GetCertificate()
        {
            X509Store sertificateStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            sertificateStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificateCollection = sertificateStore.Certificates.Find(X509FindType.FindByThumbprint, SecurityHelper.CertificateThumbprint, false);
            if (certificateCollection.Count > 0) { return certificateCollection[0]; } else return null;
        }

        internal static string CreteJWTToken()
        {
            var cert = new X509SigningCredentials(SecurityHelper.GetCertificate());
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Hulk"),
                new Claim(ClaimTypes.Spn, "superhero"),
                new Claim(ClaimTypes.Thumbprint, cert.Certificate.GetCertHashString()),
            };

            var token = new JwtSecurityToken(SecurityHelper.CertificateValidIssuer, SecurityHelper.CertificateValidAudience, claims, DateTime.UtcNow, DateTime.UtcNow.AddSeconds(10), cert);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenData = tokenHandler.WriteToken(token);

            return tokenData;
        }

    }
}