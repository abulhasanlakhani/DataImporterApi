using System.Xml;

namespace DataImporter.Business.Services
{
    public class Extractor : IExtractor
    {
        public XmlElement ExtractXmlFromEmailText(string emailText)
        {
            try
            {
                if (string.IsNullOrEmpty(emailText)) return null;

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml($"<EmailContentWrapper>{emailText}</EmailContentWrapper>");

                return xmlDoc.DocumentElement;
            }
            catch (XmlException)
            {
                return null;
            }
        }

        public XmlNode GetXmlNodeFromElement(XmlElement element, string nodeToSearch)
        {
            return element.SelectSingleNode($"//{nodeToSearch}");
        }
    }
}
