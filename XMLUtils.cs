namespace CabinetInfirmier;

using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

public static class XMLUtils
{
    public static async Task ValidateXmlFileAsync(string schemaNamespace, string xsdFilePath, string xmlFilePath) 
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(schemaNamespace, xsdFilePath);
        settings.ValidationType = ValidationType.Schema;
        Console.WriteLine("Nombre de schemas utilisés dans la validation : " + settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallback;
        var readItems = XmlReader.Create(xmlFilePath, settings);
        while (readItems.Read()) { }
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
    
    /*
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFilePath);
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
        xslt.Transform(xpathDoc, null, htmlWriter);
    }*/
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
       
        // Configuration avec XmlResolver
        XsltSettings settings = new XsltSettings(true, true); // EnableScript, EnableDocumentFunction
        XmlUrlResolver resolver = new XmlUrlResolver();
        
        xslt.Load(xsltFilePath, settings, resolver);
        xslt.Transform(xpathDoc, null, htmlWriter);
        
    }
    
    public static void XslTransform2(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
    
        // Résolveur essentiel pour document()
        XmlUrlResolver resolver = new XmlUrlResolver();
        XsltSettings settings = new XsltSettings(true, true);
    
        xslt.Load(xsltFilePath, settings, resolver);
    
        using (XmlWriter htmlWriter = XmlWriter.Create(htmlFilePath))
        {
            xslt.Transform(xpathDoc, null, htmlWriter);
        }
    }

    
}
