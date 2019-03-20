using System.Web.Mvc;
using SamlSSO.Models;
using SamlSSO.Services;

namespace SamlSSO.Controllers
{
    public class HomeController : Controller
    {
        public const string SamlResponse = "SAMLResponse";

        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("LoginSSO/{issuer}")]
        public ActionResult LoginSSO(string issuer)
        {
            var identity = SamlIdentityService.Get(issuer);

            if (identity == null)
                return new ContentResult { Content = string.Concat(@"SSO failed. \n Issuer ", issuer, " is invalid.") };

            return Redirect(string.Concat(identity.IssuerURL, SamlParam(identity.IssuerURL),
               Url.Encode(SamlService.GenerateRequest(identity, SamlService.GenerateId(), SamlService.IssueInstant()))));
        }        

        [HttpPost]
        [Route("Consume/{issuer}")]
        public ActionResult Consume(string issuer)
        {
            var response = new XmlResponse(Request.Form[SamlResponse]);
            var identity = SamlIdentityService.Get(issuer);
            if (identity == null)
                return new ContentResult { Content = string.Concat(@"SSO failed. \n Issuer ", issuer, " is invalid.") };

            if (SamlService.ResponseIsValid(response, identity))
            {
                var userId = response.GetSubject();
                if (userId == null)
                    return Redirect(identity.IssuerLogoutUrl);

                var token = SamlService.SetSsoToken(userId);
                if(token == null)
                    return new ContentResult { Content = string.Concat(@"SSO failed. \n User ", userId, " is invalid.") };

                return Redirect(string.Concat(identity.AuthenticatedRedirectUrl, "?SSOtoken=", token, "&SamlIssuer=", identity.Issuer));
            }
            return new ContentResult { Content = @"SSO failed. \n Certificate is invalid." };
        }

        private object SamlParam(string identityIssuerUrl)
        {
            return identityIssuerUrl.Contains("?") ? "&SAMLRequest=" : "?SAMLRequest=";
        }
    }
}