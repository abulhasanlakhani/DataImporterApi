using System.Xml;

namespace DataImporter.Business
{
    public interface IExtractor
    {
        XmlElement ExtractXmlFromEmailText(string emailText);
        XmlNode GetXmlNodeFromElement(XmlElement element, string nodeToSearch);
    }
}