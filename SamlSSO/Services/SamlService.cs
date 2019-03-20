using System;
using System.Deployment.Internal.CodeSigning;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using SamlSSO.Models;

namespace SamlSSO.Services
{
    public class SamlService
    {
        public static string BaseUri = "https://localhost/SamlSSO";

        public static string GenerateRequest(SamlIdentity identity, string id, string instant)
        {
            using (StringWriter writer = new StringWriter())
            {
                XmlWriterSettings writerSetting = new XmlWriterSettings { OmitXmlDeclaration = true };

                using (XmlWriter xmlWriter = XmlWriter.Create(writer, writerSetting))
                {
                    xmlWriter.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xmlWriter.WriteAttributeString("ID", id);
                    xmlWriter.WriteAttributeString("Version", "2.0");
                    xmlWriter.WriteAttributeString("IssueInstant", instant);
                    xmlWriter.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                    xmlWriter.WriteAttributeString("AssertionConsumerServiceURL", ConsumerServiceUrl(identity));
                    xmlWriter.WriteAttributeString("Destination", identity.IssuerURL);
                    xmlWriter.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xmlWriter.WriteString(BaseUri);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }

                byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(writer.ToString());
                using (MemoryStream output = new MemoryStream())
                {
                    using (DeflateStream zip = new DeflateStream(output, CompressionMode.Compress))
                        zip.Write(toEncodeAsBytes, 0, toEncodeAsBytes.Length);
                    byte[] compressed = output.ToArray();
                    return Convert.ToBase64String(compressed);
                }
            }
        }        

        public static bool ResponseIsValid(XmlResponse response, SamlIdentity identity)
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(response.Document.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            XmlNodeList nodeList = response.Document.SelectNodes("//ds:Signature", manager);
            SignedXml signedXml = new SignedXml(response.Document);
            if (nodeList == null || nodeList.Count == 0)
                return false;
            signedXml.LoadXml((XmlElement)nodeList[0]);

            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            return CheckSignature(signedXml, LoadX509Certificate(identity.Certificate));
        }

        public static string IssueInstant()
        {
            return DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static string GenerateId()
        {
            return string.Concat("_", Guid.NewGuid());
        }

        public static bool CheckSignature(SignedXml signedXml, X509Certificate2 cert)
        {
            return signedXml.CheckSignature(cert, true);
        }

        public static X509Certificate2 LoadX509Certificate(byte[] certificate)
        {
            try
            {
                return new X509Certificate2(certificate);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        private static string ConsumerServiceUrl(SamlIdentity identity)
        {
            return new Uri(string.Concat(BaseUri, "/Consume/", identity.Issuer)).ToString();
        }

        public static string SetSsoToken(string userId)
        {
            var person = UserService.Get(userId);
            if (person == null)
                return null;
            var token = Guid.NewGuid().ToString().Replace("-", string.Empty);
            person.SSOToken = token;
            UserService.Update(person);
            return token;
        }
    }
}