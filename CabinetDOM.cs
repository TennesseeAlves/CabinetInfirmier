using System.Xml;

namespace CabinetInfirmier.Csharp;
//Parseur DOM
public class CabinetDOM
{
    private XmlDocument doc;
    private XmlNode root;
    private XmlNamespaceManager nsmgr;


    public CabinetDOM(string filepath)
    {
        doc = new XmlDocument();
        doc.Load(filepath);
        root = doc.DocumentElement; //recuperer la racine
        nsmgr = new XmlNamespaceManager(doc.NameTable); //namespace
        nsmgr.AddNamespace(root.Prefix, root.NamespaceURI);
        
    }

    public XmlNodeList getXpath(string nPrefix, string nsURI, string expression)
    {
        nsmgr.AddNamespace(nPrefix, nsURI);
        return root.SelectNodes(expression, nsmgr);
    }

    public int count(string elementName)
    {
        int count = 0;
        XmlElement rootElt = (XmlElement) root;
        XmlNodeList recherche = rootElt.GetElementsByTagName(elementName);
        return recherche.Count;
    }

    public bool cabinetHasAdresse(string elementName)
    {
        bool res = true;
        string expression = "//cab:cabinet/cab:" + elementName;
        XmlNodeList node = getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", expression);

        if (node == null)
        {
            res = false;
        }

        return res;
    }
    
    //Partie Modification de l'arbre DOM et de l'instance XML
    
    

    private XmlElement MakeInfirmier(string nom, string prenom)
    {
        XmlElement infirmierElt = doc.CreateElement(root.Prefix, "infirmier", root.NamespaceURI);
        int coundId = count("infirmier") + 1;
        Console.WriteLine(coundId);
        string id = "00" + coundId.ToString();
        string photo = prenom + ".png"; 
        infirmierElt.SetAttribute("id", id);
        
        XmlElement nomElt = doc.CreateElement(root.Prefix, "nom", root.NamespaceURI);
        XmlText nomTxt = doc.CreateTextNode(nom);
        nomElt.AppendChild(nomTxt);
        
        XmlElement prenomElt = doc.CreateElement(root.Prefix, "prénom", root.NamespaceURI);
        XmlText prenomTxt = doc.CreateTextNode(prenom);
        prenomElt.AppendChild(prenomTxt);
        
        XmlElement photoElt = doc.CreateElement(root.Prefix, "photo", root.NamespaceURI);
        XmlText photoTxt = doc.CreateTextNode(photo);
        photoElt.AppendChild(photoTxt);
        
        infirmierElt.AppendChild(nomElt);
        infirmierElt.AppendChild(prenomElt);
        infirmierElt.AppendChild(photoElt);
        
        return infirmierElt;
    }

    public void addInfirmier(string nom, string prenom)
    {
        XmlElement newInfirmierElt = MakeInfirmier(nom, prenom);
        root.AppendChild(newInfirmierElt);
        
    }

}