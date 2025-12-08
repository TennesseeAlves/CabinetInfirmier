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
        
        string id = "00" + coundId.ToString();
        Console.WriteLine(coundId);
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
        string chemin = "../../data/newCabinet.xml";
        //doc.Save(chemin);
        //Console.WriteLine($"Écrit à : {Path.GetFullPath(chemin)}");
        
    }

    private XmlElement MakePatient(string nom, string prenom, int jour, string nss, Adresse adresse)
    {
        XmlElement patientElt = doc.CreateElement(root.Prefix, "patient", root.NamespaceURI);
        
        XmlElement nomElt = doc.CreateElement(root.Prefix, "nom", root.NamespaceURI);
        XmlText nomTxt = doc.CreateTextNode(nom);
        nomElt.AppendChild(nomTxt);
        
        XmlElement prenomElt = doc.CreateElement(root.Prefix, "prenom", root.NamespaceURI);
        XmlText prenomTxt = doc.CreateTextNode(prenom);
        prenomElt.AppendChild(prenomTxt);

        string sexe = "";
        if (nss[0] == '1')
        {
            sexe = "M";
        }
        else if (nss[0] == '2')
        {
            sexe = "F";
        }
        XmlElement sexeElt =  doc.CreateElement(root.Prefix, "sexe", root.NamespaceURI);
        XmlText sexeTxt = doc.CreateTextNode(sexe);
        sexeElt.AppendChild(sexeTxt);

        string date = "";
        string annee =  "" + nss[1] + nss[2];
        int anneeInt = int.Parse(annee);
        if (anneeInt > 25)
        {
            annee = "19" + annee;
        }
        else
        {
            annee = "20" + annee;
        }
        
        
        return patientElt;
    }
    

}