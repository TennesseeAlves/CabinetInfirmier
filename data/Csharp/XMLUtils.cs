using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace CabinetInfirmier.data.Csharp;

public class XMLUtils
{
    
    public static async Task ValidateXmlFileAsync(string schemaNamespace, string xsdFilePath, string xmlFilePath) 
    {
        
        var settings = new XmlReaderSettings();
        
        settings.Schemas.Add(schemaNamespace, xsdFilePath);
        Console.WriteLine("coucou");
        settings.ValidationType = ValidationType.Schema;
        
        
        Console.WriteLine("Nombre de schemas utilisés dans la validation : " + settings.Schemas.Count);

        settings.ValidationEventHandler += ValidationCallback;
    
        using (var readItems = XmlReader.Create(xmlFilePath, settings)) 
        {
            while (readItems.Read()) 
            {
                // Lecture complète du fichier pour déclencher la validation
            }
        }
    }

    private static void ValidationCallback(object? sender, ValidationEventArgs e) 
    {
        if (e.Severity.Equals(XmlSeverityType.Warning)) 
        {
            Console.Write("WARNING: ");
            Console.WriteLine(e.Message);
        } 
        else if (e.Severity.Equals(XmlSeverityType.Error)) 
        {
            Console.Write("ERROR: ");
            Console.WriteLine(e.Message);
        }
    }
    
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFilePath);
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
        xslt.Transform(xpathDoc, null, htmlWriter);
    }
    
}