using System.Xml;
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
        //int actes = Cabinet.countActes("./data/xml/cabinet.xml");


        //Parseur DOM :
        CabinetDOM cab = new CabinetDOM("./data/xml/cabinet.xml");

        String xpathExpression = "//cab:infirmier";
        XmlNodeList gator = cab.getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", xpathExpression);
        Console.WriteLine("Combien de patient : {0}", gator.Count);

        string inf = "infirmier";
        int comptInf = cab.count(inf);
        Console.WriteLine("-> Nombre de {0} : {1}", inf, comptInf);

        string pat = "patient";
        int comptPat = cab.count(pat);
        Console.WriteLine("-> Nombre de {0} : {1}", pat, comptPat);

        string adr = "adresse";
        bool res = cab.cabinetHasAdresse(adr);
        Console.WriteLine("-> 3 patients ?? : {0}", res);

        //test Partie modification de l'arbre

        cab.addInfirmier("Némard", "Jean");


        Console.WriteLine("Infirmier apres ajout : {0}", cab.count("infirmier"));
        XmlNodeList InfirmiersList =
            cab.getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", "//cab:infirmier");
        foreach (XmlNode n in InfirmiersList)
        {
            XmlNode nom = n.SelectSingleNode("nom");
            Console.WriteLine("nom");

        }

    }
}    