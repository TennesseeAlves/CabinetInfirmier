namespace cabinetInfirmier;

using System.Xml;

public class Program {
    public static void Main(string[] args)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("data/xml/fichePatient.xml");
        XmlNode root = doc.DocumentElement;
        // Add namespace
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("" ,"http://www.univ-grenoble-alpes.fr/l3miage/patient") ;

        XmlNode nomNode = ((XmlElement)root).GetElementsByTagName("nom").Item(1); // selectionne le noeud
        String nomString = nomNode.InnerText;
        Console.WriteLine(nomString);
        Console.WriteLine(nomNode.Name);
        Console.WriteLine(nomNode.NodeType);
    }
}