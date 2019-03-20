using System.Text;
using SamlSSO.Models;

namespace SamlSSO.Services
{
    public class SamlIdentityService
    {
        public static SamlIdentity Get(string issuer)
        {
            if (issuer == "MediaSuite")
            {
                return new SamlIdentity
                {
                    Issuer = "MediaSuite",
                    IssuerURL = "https://accounts.google.com/o/saml2/idp?idpid=C0157hhlc",
                    AuthenticatedRedirectUrl = "https://localhost/SamlSSO/LoggedIn/GoodJob",
                    IssuerLogoutUrl = "http://mediasuite.co.nz",
                    Certificate = Encoding.ASCII.GetBytes(@"-----BEGIN CERTIFICATE-----
MIIDdDCCAlygAwIBAgIGAVpIUv7CMA0GCSqGSIb3DQEBCwUAMHsxFDASBgNVBAoTC0dvb2dsZSBJ
bmMuMRYwFAYDVQQHEw1Nb3VudGFpbiBWaWV3MQ8wDQYDVQQDEwZHb29nbGUxGDAWBgNVBAsTD0dv
b2dsZSBGb3IgV29yazELMAkGA1UEBhMCVVMxEzARBgNVBAgTCkNhbGlmb3JuaWEwHhcNMTcwMjE2
MTkwNzU4WhcNMjIwMjE1MTkwNzU4WjB7MRQwEgYDVQQKEwtHb29nbGUgSW5jLjEWMBQGA1UEBxMN
TW91bnRhaW4gVmlldzEPMA0GA1UEAxMGR29vZ2xlMRgwFgYDVQQLEw9Hb29nbGUgRm9yIFdvcmsx
CzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpDYWxpZm9ybmlhMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A
MIIBCgKCAQEA0V+j5SDDFbGwFflw9tzOkewteS5qwLi+iKb0wTAMybBzSqMpRUPyZLnRRkoK/rWU
+3MSTPmzq02cRa3A6EMTxqmwF1xPLNAxtqNvfJ+M4rxH7VgxgY9xnkIRdA3tp0/esWxR0ktCG45o
j8puYZJ9sdEasDzNhjwm/ubm8vHfxRxUI+2bdu2k0mg1PIKoNgysQNxMr6B+uMBWBPYFi8kvTgvS
piWmWQ3ZSUcPTn8AjQwC6AL8cfFIdzYju4sYS2kAQmRPjIPtqDIOrb9zgK4K+eP7UbWihxOqSLX8
4HSnR7PJSNITjmyxOHXjkijDttYfCFrP87LMPTiEyPck3/IqcQIDAQABMA0GCSqGSIb3DQEBCwUA
A4IBAQAIi8FGlo7S20sBXLEbQx/nbw5MnDybNxghawBhiWlA0V/H+JP7ujVIYPLbmN77JhSWKnFw
5pu1eBop/fEZ35FBhcADReadL4yv1jgMMGz2BESTMsO06BVDOBIkLXpav7Lp0KonTrXKoWVsEZrW
UKesi4VfhYeO9kkz0ZcFtihSvVn3h+a+DlIkQ0R3wXMDpzXLUiiFSpl69Ip3ocTWjBnzh/ptJzO5
AvjBIC8rOjS0YN3Om9WG5v9tAXQKu667b+IV/7ocbWwHMQWpP8E431X8BKScCeyWTXcht0a2BXM9
kYnKf876o/NkLQYTdGprOxWH9MBARCw6wQaignz3QCpU
-----END CERTIFICATE-----")
                };
            }
            return null;
        }
    }
}