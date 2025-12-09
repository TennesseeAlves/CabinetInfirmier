using System.Xml;
using CabinetInfirmier;

using CabinetInfirmier.Csharp;

class Program
{

    static void Main(string[] args)
    {
        //Validation du fichier XML Cabinet
        //Console.WriteLine("Validation fichiers Cabinet");
        //XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical", "../../../data/xsd/cabinet.xsd", "../../../data/xml/cabinet.xml" );


        //Xslt transformation : 
        Console.Write("Transformation Page Infirmere");
        XMLUtils.XslTransform2("../../../data/xml/cabinet.xml", "../../../data/xslt/pageInfirmiere.xsl", "../../../data/html/pageInfirmiere.html");

        //Essaie XmlReader :
        //Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");

        // Cabinet.AnalyseNomsInfirmiers("./data/xml/cabinet.xml");
        //Cabinet.AnalyseNoms("./data/xml/cabinet.xml", "nom");

        //Compte le nombre d'actes differents necessaires, tout patient confondus.
        //int actes = Cabinet.countActes("./data/xml/cabinet.xml");


        //Parseur DOM :

        /*string filename = "./data/xml/cabinet.xml";
            
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

        //cab.addInfirmier("Némard", "Jean");
        Console.WriteLine("Nouveau infirmier ajouté");


        Adresse newAdresse = new Adresse("KIKS", 0, 0, "rue de la paix", 38100, "Chicagre");
        cab.addPatient("KIKS", "Burhan", "2000-03-03", "102039999988876", newAdresse);
        Console.WriteLine("Nouveau patient ajouté");

        cab.addVisite("2026-01-03", 003, 101, "KIKS");
        Console.WriteLine("Ajout de visite de KIKS");
        
        // TEST methode nssValide(nomPatient) qui verifie que le numero de securite social de nomPatient est valide par rapport aux informations

        bool resultNSS = cab.nssValide("BARKOK");
        Console.WriteLine("Test Omar: {0}", resultNSS);*/
        
        
        // Partie serealisation 
        
        Console.WriteLine("Debut serealisation de Adresse");

        var adrManager = new XMLManager<AdresseSerealisation>();
        
        var  TestAdresse = new AdresseSerealisation(10, 10, "rue de la paix", "69000", "Lyon");

        string path = "../../../data/perso/adresse.xml";
        adrManager.Save(path, TestAdresse);
        Console.WriteLine("Serealisation de Adresse effectué");
        

    }
}    