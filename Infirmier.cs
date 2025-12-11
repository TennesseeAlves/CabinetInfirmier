using System.Xml.Serialization;

namespace CabinetInfirmier.Csharp;


[XmlRoot("infirmier", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]

public class Infirmier
{
    [XmlAttribute("id")] public string Id { get; set; }
    
    [XmlElement("nom")] public string Nom { get; set; }
    
    [XmlElement("prénom")] public string Prenom { get; set; }
    
    [XmlElement("photo")] public string Photo { get; set; }
    
    

    public Infirmier(){}

    public Infirmier(string id, string nom, string prenom, string photo)
    {
        this.Id = id;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Photo = photo;
    }

    public string toString()
    {
        String res = "=> Infirmier : \nNom: " + Nom + "\nPrenom : " + Prenom + "\nPhoto : " + Photo + "\nId : " + Id;
        return res;
        
    }
}