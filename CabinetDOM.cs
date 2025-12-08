using System.Text;
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
        doc.PreserveWhitespace = false; // TODO: mofication de l'indentation
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

    private string getExpressionDansNss(string nomPatient, string elementName)
    {
        string expr = "//cab:patient[contains(./cab:nom/text(),'" + nomPatient + "')]/cab:" + elementName;
        return expr;
    }

    private XmlNodeList getXpathPerso(string expression)
    {
        XmlNodeList node = getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", expression);
        return node;
    }
    public bool nssValide(string nomPatient)
    {
        Console.WriteLine("Verification du nss du patient {0}",  nomPatient);
        bool res = true;
        string expression = "//cab:patient[contains(./cab:nom/text(),'" + nomPatient + "')]/cab:numéro";
        XmlNodeList noeudlist = getXpathPerso(expression);
        if (noeudlist.Count != 0)
        {
            XmlNode nssNode = noeudlist[0];
            string nss =  nssNode.InnerText;
            int tailleNss = nss.Length;
            char sexe = '4';
            if (nss[0] == '1')
            {
                sexe = 'M';
            }
            else if (nss[0] == '2')
            {
                sexe = 'F';
            }
            string anneeNaiss = "" + nss[1] + nss[2];
            string moisNaiss = "" + nss[3] + nss[4];
            //Test des valeurs TODO: peut etre à enlever
            Console.WriteLine("NSS du patient : {0} de taille {1}", nss, tailleNss);
            Console.WriteLine("Infos d'apres le NSS :  \n-> Son sexe : {0} \n-> son annee : {1} \n-> son mois : {2}", sexe, anneeNaiss, moisNaiss);
            
            
            var sexeNss  = getXpathPerso(getExpressionDansNss(nomPatient, "sexe"))[0].InnerText[0];
            var naissance  =  getXpathPerso(getExpressionDansNss(nomPatient, "naissance"))[0].InnerText;
            string anneeNaissNss = "" + naissance[2] + naissance[3];
            string moisNaissNss = "" + naissance[5] + naissance[6];
            
            //TODO: test peut etre à enlever
            Console.WriteLine("Date de naissance : {0}", naissance);
            Console.WriteLine("Infos de annee naissance : {0}", anneeNaissNss);
            Console.WriteLine("Infos de mois naissance : {0}",  moisNaissNss);
            Console.WriteLine("Infos de sexe: {0}", sexeNss);
            
            if (tailleNss != 15 || (sexe != sexeNss) || !anneeNaiss.Equals(anneeNaissNss) ||
                !moisNaiss.Equals(moisNaissNss))
            {
                res = false;
            }
            
        }
        else
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
        
        string id = "00" + coundId;
        Console.WriteLine(coundId);
        string photo = prenom.ToLower() + ".png"; 
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
        XmlElement rootElt = (XmlElement) root;
        var cabLocation = rootElt.GetElementsByTagName("infirmiers").Item(0);
        cabLocation.AppendChild(newInfirmierElt);
        
        string chemin = "../../../data/xml/newCabinet.xml";
        doc.Save(chemin); //Modification de l'instance XML (nouveau doc : newCabinet.xml)
        // TODO: modif de l'indentation

    }

    private XmlElement MakePatient(string nom, string prenom, string dateNaissance, string nss, Adresse adresse)
    {
        XmlElement patientElt = doc.CreateElement(root.Prefix, "patient", root.NamespaceURI);
        
        XmlElement nomElt = doc.CreateElement(root.Prefix, "nom", root.NamespaceURI);
        XmlText nomTxt = doc.CreateTextNode(nom);
        nomElt.AppendChild(nomTxt);
        
        XmlElement prenomElt = doc.CreateElement(root.Prefix, "prénom", root.NamespaceURI);
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

        XmlElement naissElt = doc.CreateElement(root.Prefix, "naissance", root.NamespaceURI);
        XmlText naissTxt = doc.CreateTextNode(dateNaissance);
        naissElt.AppendChild(naissTxt);
        
        XmlElement nssElt = doc.CreateElement(root.Prefix, "numéro", root.NamespaceURI);
        XmlText nssTxt = doc.CreateTextNode(nss);
        nssElt.AppendChild(nssTxt);
        
        XmlElement adresseElt = doc.CreateElement(root.Prefix, "adresse", root.NamespaceURI);
        if (adresse.getEtage() != 0)
        {
            XmlElement etageElt = doc.CreateElement(root.Prefix, "étage", root.NamespaceURI);
            XmlText etageTxt = doc.CreateTextNode(adresse.getEtage().ToString());
            etageElt.AppendChild(etageTxt);
            adresseElt.AppendChild(etageElt);
        }

        if (adresse.getNumero() != 0)
        {
            XmlElement numeroElt = doc.CreateElement(root.Prefix, "numéro", root.NamespaceURI);
            XmlText numeroTxt = doc.CreateTextNode(adresse.getNumero().ToString());
            numeroElt.AppendChild(numeroTxt);
            adresseElt.AppendChild(numeroElt);
        }
        
        XmlElement rueElt =  doc.CreateElement(root.Prefix, "rue", root.NamespaceURI);
        XmlText rueTxt = doc.CreateTextNode(adresse.getRue());
        rueElt.AppendChild(rueTxt);
        adresseElt.AppendChild(rueElt);
        
        XmlElement codePostElt = doc.CreateElement(root.Prefix, "codePostal", root.NamespaceURI);
        XmlText codePostTxt = doc.CreateTextNode(adresse.getCodePostal().ToString());
        codePostElt.AppendChild(codePostTxt);
        adresseElt.AppendChild(codePostElt);
        
        XmlElement villeElt = doc.CreateElement(root.Prefix, "ville", root.NamespaceURI);
        XmlText villeTxt = doc.CreateTextNode(adresse.getVille());
        villeElt.AppendChild(villeTxt);
        adresseElt.AppendChild(villeElt);
        
        patientElt.AppendChild(nomElt);
        patientElt.AppendChild(prenomElt);
        patientElt.AppendChild(sexeElt);
        patientElt.AppendChild(naissElt);
        patientElt.AppendChild(nssElt);
        patientElt.AppendChild(adresseElt);
        
        return patientElt;
    }


    public void addPatient(string nom, string prenom, string dateNaissance, string nss, Adresse adresse)
    {
        XmlElement newPatientElt = MakePatient(nom, prenom, dateNaissance, nss, adresse);
        XmlElement rootElt = (XmlElement) root;
        var cabLocation = rootElt.GetElementsByTagName("patients").Item(0);
        cabLocation.AppendChild(newPatientElt);
        
        string chemin = "../../../data/xml/newCabinet.xml";
        //doc.Save(chemin); //Modification de l'instance XML (nouveau doc : newCabinet.xml)

        /*XmlTextWriter writer = new XmlTextWriter(chemin, Encoding.UTF8); // TODO: modif de l'indentation
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;
        doc.Save(writer);*/
    }

    private XmlElement makeVisite(string date, int intervenant, int acteId)
    {
        XmlElement visiteElt = doc.CreateElement("visite", root.NamespaceURI);

        visiteElt.SetAttribute("date", date);
        visiteElt.SetAttribute("intervenant", "00" + intervenant);
        
        XmlElement acteElt = doc.CreateElement("acte", root.NamespaceURI);
        acteElt.SetAttribute("id", acteId.ToString());
        visiteElt.AppendChild(acteElt);
        
        return visiteElt;
    }

    public void addVisite(string date, int intervenant, int acteId, string nomPatient)
    {
        XmlElement newVisiteElt = makeVisite(date, intervenant, acteId);

        string expression = "//cab:patient[contains(./cab:nom/text(),'" + nomPatient + "')]"; 
        XmlNodeList patientNode = getXpath("cab", "http://www.univ-grenoble-alpes.fr/l3miage/medical", expression);

        if (patientNode.Count != 0)
        {
            Console.WriteLine("Entre condition");
            patientNode[0].AppendChild(newVisiteElt);
            string chemin = "../../../data/xml/newCabinet.xml"; 
            doc.Save(chemin); //Modification de l'instance XML (nouveau doc : newCabinet.xml)
        }
        
        
    }
    

}