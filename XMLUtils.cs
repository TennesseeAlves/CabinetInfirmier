namespace CabinetInfirmier;

using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;


/*// Attention : cette version de la classe XMLUtils est fournie par une LLM (donc le code que j'ai copié je ne le maîtrise pas totalement)
public static class XMLUtils
{
    public static async Task ValidateXmlFileAsync(string schemaNamespace, string xsdFilePath, string xmlFilePath)
    {
        var settings = new XmlReaderSettings();
        settings.ValidationType = ValidationType.Schema;
        settings.ValidationEventHandler += ValidationCallBack;

        // Add main schema (cabinet.xsd)
        settings.Schemas.Add(schemaNamespace, xsdFilePath);

        // Add imported schema (actes.xsd) - assumes it's in the same directory as cabinet.xsd
        string actesNamespace = "http://www.univ-grenoble-alpes.fr/l3miage/actes";
        string actesXsdPath = Path.Combine(Path.GetDirectoryName(xsdFilePath) ?? string.Empty, "actes.xsd");
        if (File.Exists(actesXsdPath))
        {
            settings.Schemas.Add(actesNamespace, actesXsdPath);
        }
        else
        {
            Console.WriteLine($"Warning: actes.xsd not found at {actesXsdPath}. Validation may fail.");
        }

        Console.WriteLine("Nombre de schemas utilisés dans la validation : " + settings.Schemas.Count); // Should print 2 if actes.xsd exists

        try
        {
            using var readItems = XmlReader.Create(xmlFilePath, settings);
            while (await readItems.ReadAsync()) { } // Async read for better performance
            Console.WriteLine("Validation completed successfully.");
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            // You can rethrow or handle as needed
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during validation: {ex.Message}");
        }
    }

    private static void ValidationCallBack(object? sender, ValidationEventArgs e)
    {
        if (e.Severity.Equals(XmlSeverityType.Warning))
        {
            Console.Write("WARNING: ");
        }
        else if (e.Severity.Equals(XmlSeverityType.Error))
        {
            Console.Write("ERROR: ");
        }
        Console.WriteLine(e.Message);
    }

    public static async Task XslTransformAsync(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        try
        {
            // Ensure output directory exists
            string htmlDir = Path.GetDirectoryName(htmlFilePath);
            if (!string.IsNullOrEmpty(htmlDir) && !Directory.Exists(htmlDir))
            {
                Directory.CreateDirectory(htmlDir);
            }

            await Task.Run(() =>
            {
                using var xpathDoc = new XPathDocument(xmlFilePath);
                var xslt = new XslCompiledTransform();
                xslt.Load(xsltFilePath);

                using var htmlWriter = new XmlTextWriter(htmlFilePath, null);
                xslt.Transform(xpathDoc, null, htmlWriter);
            });

            Console.WriteLine($"Transformation completed. Output saved to: {htmlFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transformation failed: {ex.Message}");
        }
    }
} // end of class
*/







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
    
    
    /*public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
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
