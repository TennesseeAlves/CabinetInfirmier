using CabinetInfirmier;

using CabinetInfirmier.Csharp;

class Program
{

    static void Main(string[] args)
    {
        //Validation du fichier XML Cabinet
        //Console.WriteLine("Validation fichiers Cabinet");
        //XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical", "./data/xsd/cabinet.xsd", "./data/xml/cabinet.xml");
        
        
        //Xslt transformation : 
        //Console.Write("Transformation Page Infirmere");
        //XMLUtils.XslTransform("./data/xml/cabinet.xml", "./data/xslt/pageInfirmiere.xsl", "./data/html/pageInfirmiere.html");
        
        //Essaie XmlReader :
        //Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");
        
       // Cabinet.AnalyseNomsInfirmiers("./data/xml/cabinet.xml");
        //Cabinet.AnalyseNoms("./data/xml/cabinet.xml", "nom");
        
        //Compte le nombre d'actes differents necessaires, tout patient confondus.
        int actes = Cabinet.countActes("./data/xml/cabinet.xml");

    }

}