using System.Xml;
using System.Xml.Serialization;
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
        //Console.Write("Transformation Page Infirmere");
        //XMLUtils.XslTransform2("../../../data/xml/cabinet.xml", "../../../data/xslt/pageInfirmiere.xsl", "../../../data/html/pageInfirmiere.html");

        
        //Partie parseur XmlReader :
        
        //Cabinet.AnalyseGlobale("./data/xml/cabinet.xml");

        //Cabinet.AnalyseNomsInfirmiers("./data/xml/cabinet.xml");
        //Cabinet.AnalyseNoms("./data/xml/cabinet.xml", "nom");

        //Compte le nombre d'actes differents necessaires, tout patient confondus.
        //int actes = Cabinet.countActes("./data/xml/cabinet.xml");
        


        //Partie parseur DOM :

        string filename = "./data/xml/cabinet.xml";
            
        CabinetDOM cab = new CabinetDOM("./data/xml/cabinet.xml");
        /*
        String xpathExpression = "//cab:infirmier";
        XmlNodeList nbInf = cab.getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", xpathExpression);
        Console.WriteLine("Combien de patient : {0}", nbInf.Count);

        string inf = "infirmier";
        int nbInfirmier = cab.count(inf);
        Console.WriteLine("-> Nombre de {0} : {1}", inf, nbInfirmier);

        string pat = "patient";
        int nbPatient = cab.count(pat);
        Console.WriteLine("-> Nombre de {0} : {1}", pat, nbPatient);
        
        
        bool res = cab.adresseCabinetEstComplete();
        Console.WriteLine("-> Adresse Cabinet complète ?? : {0}", res);

        String nomTestPatient = "Pien"; //Nom du patient dont on veut verifier l'adresse
        bool resPatient = cab.adressePatientEstComplete(nomTestPatient);
        Console.WriteLine("-> Adresse du patient '{0}' complète ?? : {1}", nomTestPatient, resPatient);
        */

        //test Partie modification de l'arbre
        
        //On ne peut pas ajouter un nouvel infirmier et un patient à la fois (soit l'un soit l'autre), une exception est levée sinon... 
        
        //cab.addInfirmier("Némard", "Jean");
        //Console.WriteLine("Nouveau infirmier ajouté");

/*
        AdresseSerealisation newAdresse = new AdresseSerealisation(5, 6, "rue de la paix", "38100", "Chicagre");
        cab.addPatient("KIKS", "Burhan", "2000-03-03", "102039999988876", newAdresse);
        Console.WriteLine("Nouveau patient ajouté");

        List<int> listActeId = new List<int>();
        listActeId.Add(101);
        listActeId.Add(102);
        listActeId.Add(103);
        cab.addVisite("2026-01-03", 003, listActeId, "KIKS");
        Console.WriteLine("Ajout de visite de KIKS");

        // TEST methode nssValide(nomPatient) qui verifie que le numero de securite social de nomPatient est valide par rapport aux informations

        bool resultNSS = cab.nssValide("BARKOK");
        Console.WriteLine("Test Omar: {0}", resultNSS);
        */


        // Partie serealisation 

        Console.WriteLine("Debut serealisation de Adresse");

        var adrManager = new XMLManager<AdresseSerealisation>();
        var  TestAdresse = new AdresseSerealisation(12, 1, "rue de la paix", "69000", "Lyon");
        string pathAdr = "../../../data/perso/adresse.xml";
        adrManager.Save(pathAdr, TestAdresse);
        Console.WriteLine("Serealisation de Adresse effectué");
        
        var infirManager = new XMLManager<InfirmierSerealisation>();
        var TestInfirmier = new InfirmierSerealisation(005, "BARKOK", "Omar", "Omar.png");
        string pathInfir = "../../../data/perso/infirmier.xml";
        infirManager.Save(pathInfir, TestInfirmier);
        Console.WriteLine("Serealisation de Infirmier effectué");
        
        var InfirmiersManager = new XMLManager<InfirmiersSerealisation>();
        List<InfirmierSerealisation> listInfirmiers = new List<InfirmierSerealisation>();
        listInfirmiers.Add(TestInfirmier);
        var infirmiersSer = new InfirmiersSerealisation(listInfirmiers);
        string pathInfirmiers = "../../../data/perso/infirmiers.xml";
        InfirmiersManager.Save(pathInfirmiers, infirmiersSer);
        Console.WriteLine("Serealisation de Infirmier effectué");
        
        
        
        




    }
}    