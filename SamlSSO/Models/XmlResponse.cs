using System;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace SamlSSO.Models
{
    public class XmlResponse
    {
        public XmlResponse(string xml)
        {
            var enc = new ASCIIEncoding();
            Document = new XmlDocument { PreserveWhitespace = true };
            Document.LoadXml(enc.GetString(Convert.FromBase64String(xml)));
        }

        public XmlDocument Document { get; set; }

        public string GetSubject()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(Document.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            XmlNode node = Document.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);
            return node != null ? node.InnerText : null;
        }
    }
}