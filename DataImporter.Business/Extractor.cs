using System.Xml;

namespace DataImporter.Business
{
    public class Extractor : IExtractor
    {
        public XmlElement ExtractXmlFromEmailText(string emailText)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($"<EmailContentWrapper>{emailText}</EmailContentWrapper>");

            return xmlDoc.DocumentElement;
        }

        public XmlNode GetXmlNodeFromElement(XmlElement element, string nodeToSearch)
        {
            return element.SelectSingleNode($"//{nodeToSearch}");
        }
    }
}
