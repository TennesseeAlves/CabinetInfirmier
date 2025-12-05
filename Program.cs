using CabinetInfirmier.data.Csharp;
using CabinetInfirmier;
class Program
{

    static void Main(string[] args)
    {
        //Validation des fichier XML Cabinet
        Console.WriteLine("Validation fichiers Cabinet");
        XMLUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/medical", "../xsd/cabinet.xsd",
                "../xml/cabinet.xml");
        
        //Xslt transformation : 
        //Console.Write("Transformation Page Infirmere");
        //XMLUtils.XslTransform("../xml/Cabinet.xml", "../xslt/PageInfirmiere.xsl", "../html/PageInfirmiere.html");
    }

}