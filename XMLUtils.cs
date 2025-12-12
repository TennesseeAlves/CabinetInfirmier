using System.Text;

namespace CabinetInfirmier;

using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

public static class XMLUtils
{
    //nouvelle methode (IA) qui attend un xmlFilePath et un ou plusieurs couples de (namespaceName et un xsdpath associé)
    public static void ValidateXmlFileAsync(string xmlFilePath, params (string namespaceName, string xsdPath)[] schemas)
    {
        var settings = new XmlReaderSettings();
    
        // Charger tous les schémas
        foreach (var (namespaceName, xsdPath) in schemas)
        {
            settings.Schemas.Add(namespaceName, xsdPath);
        }
    
        settings.ValidationType = ValidationType.Schema;
        Console.WriteLine("Nombre de schemas utilisés dans la validation : " + settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallback;
    
        using (var readItems = XmlReader.Create(xmlFilePath, settings))
        {
            while (readItems.Read()) { }
        }
    
        Console.WriteLine("Validation terminée.");
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
        try
        {
            // Validation des chemins
            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException($"Le fichier XML n'existe pas : {xmlFilePath}");
        
            if (!File.Exists(xsltFilePath))
                throw new FileNotFoundException($"Le fichier XSLT n'existe pas : {xsltFilePath}");

            // Chargement du document XML
            XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        
            // Configuration XSLT avec activation de la fonction document()
            XslCompiledTransform xslt = new XslCompiledTransform();
            XsltSettings settings = new XsltSettings(enableDocumentFunction: true, enableScript: false);
            XmlUrlResolver resolver = new XmlUrlResolver();
        
            // Chargement du XSLT avec les paramètres de sécurité
            xslt.Load(xsltFilePath, settings, resolver);

            // Transformation AVEC le resolver pour permettre l'accès aux documents externes
            using (XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8))
            {
                htmlWriter.Formatting = Formatting.Indented;
                // IMPORTANT: Passer le resolver ici aussi
                xslt.Transform(xpathDoc, null, htmlWriter, resolver);
            }

            Console.WriteLine($"Transformation réussie : {htmlFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la transformation : {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Erreur interne : {ex.InnerException.Message}");
            }
            throw;
        }
    }

    public static void XslTransform2(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        try
        {
            // Validation des chemins
            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException($"Le fichier XML n'existe pas : {xmlFilePath}");
        
            if (!File.Exists(xsltFilePath))
                throw new FileNotFoundException($"Le fichier XSLT n'existe pas : {xsltFilePath}");

            // Chargement et transformation
            XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltFilePath);

            // Utilisation de 'using' pour libérer automatiquement les ressources
            using (XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null))
            {
                htmlWriter.Formatting = Formatting.Indented;
                xslt.Transform(xpathDoc, null, htmlWriter);
            }

            Console.WriteLine($"Transformation réussie : {htmlFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la transformation : {ex.Message}");
            throw;
        }
    }
    
    /*public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFilePath);
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
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
    */
}
