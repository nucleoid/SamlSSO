
namespace SamlSSO.Models
{
    public class SamlIdentity
    {
        public string IssuerURL { get; set; }
        public string Issuer { get; set; }
        public string AuthenticatedRedirectUrl { get; set; }
        public string IssuerLogoutUrl { get; set; }
        public byte[] Certificate { get; set; }
    }
}